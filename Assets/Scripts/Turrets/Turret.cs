using System.Collections.Generic;
using TDTest.Time;
using UnityEngine;

namespace TDTest.Combat
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] SphereCollider sphereCollider;

        TurretDescription description;
        TurretSkeleton skeleton;
        Timer shootTimer;
        Transform head;

        List<Enemy> registeredEnemies;
        Enemy target;

        TurretFSM fsm;

        public void Initialize(TurretDescription newDescription)
        {
            description = newDescription;
            shootTimer = new()
            {
                IsRepeating = true,
                IsScaled = true,
            };
            shootTimer.OnComplete += Shoot;

            skeleton = Instantiate(description.SkeletonPrefab, transform);
            head = skeleton.Head;

            registeredEnemies = new();
            target = null;

            fsm = new();
            fsm.StateMachine.Configure(TurretFSM.State.Idle)
                .OnEntry(OnIdleEnter);
            fsm.StateMachine.Configure(TurretFSM.State.Fighting)
                .OnEntry(OnFightEnter);

            sphereCollider.radius = description.Radius;
        }

        void OnTriggerEnter(Collider other)
        {
            var enemy = other.transform.GetComponent<Enemy>();
            Debug.Assert(enemy != null, "Turret: Triggered for something other than enemy layer");
            Debug.Assert(!registeredEnemies.Contains(enemy), "Turret: Enemy was already registered");

            registeredEnemies.Add(enemy);
            HandleEnemyRegistrationChange();
        }

        void OnTriggerExit(Collider other)
        {
            var enemy = other.transform.GetComponent<Enemy>();
            Debug.Assert(enemy != null, "Turret: Triggered for something other than enemy layer");
            Debug.Assert(registeredEnemies.Contains(enemy), "Turret: Enemy was not registered");

            registeredEnemies.Remove(enemy);
            HandleEnemyRegistrationChange();
        }

        void HandleEnemyRegistrationChange()
        {
            if (registeredEnemies.Count == 0)
            {
                target = null;
                fsm.StateMachine.Signal(TurretFSM.Trigger.NoTarget);
            }
            else
            {
                target = registeredEnemies[0];
                fsm.StateMachine.Signal(TurretFSM.Trigger.TargetFound);
            }
        }
        
        void OnIdleEnter()
        {
            shootTimer.Pause();
            head.LookAt(head.position + Vector3.up * 5f);
        }

        void OnFightEnter()
        {
            shootTimer.Start(description.ShootDelay);
            Shoot();
        }

        void Shoot()
        {
            if (target == null)
            {
                registeredEnemies.Remove(target);
                HandleEnemyRegistrationChange();
                return;
            }

            Debug.Assert(registeredEnemies.Contains(target), "Tried to shoot at unregistered enemy");

            head.LookAt(target.transform);
            skeleton.ShootRenderer.ShootAt(target.transform);

            target.Health.Damage(description.Damage, KillTarget);
        }

        void KillTarget()
        {
            registeredEnemies.Remove(target);
            Statics.Combat.KillEnemy(target);
            target = null;

            HandleEnemyRegistrationChange();
        }
    }
}
