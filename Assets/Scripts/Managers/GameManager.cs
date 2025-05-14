using System.Collections.Generic;
using TDTest.Building;
using TDTest.Combat;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace TDTest
{
    [DefaultExecutionOrder(-10)]
    public class GameManager : MonoBehaviour
    {
        [SerializeField] int startGold;

        [Space(5)]

        [SerializeField] Enemy levelEnemyPrefab;
        [SerializeField] Turret levelTurretPrefab;
        [SerializeField] PreviewTurret levelPreviewTurretPrefab;
        [SerializeField] List<TurretDescription> levelAvailableTurrets;

        void Start()
        {
            Statics.Initialize();
            Statics.Combat.SetEnemyPrefab(levelEnemyPrefab);

            Statics.Build.SetAvailableTurrets(levelAvailableTurrets);
            Statics.Build.SetTurretPrefab(levelTurretPrefab);
            Statics.Build.SetPreviewTurretPrefab(levelPreviewTurretPrefab);

            Statics.Gold.AddGold(startGold);
        }

        void Update()
        {
            Statics.Tick(UnityEngine.Time.deltaTime, UnityEngine.Time.unscaledTime);
        }

        void OnDestroy()
        {
            Statics.Deinitialize();
        }
    }
}