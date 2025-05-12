using UnityEngine;

namespace TDTest.Combat
{
    public class Enemy : MonoBehaviour
    {
        public Health Health { get; private set; }
        public EnemyDescription Description { get; private set; }
        bool isInitialized = false;

        public void Initialize(EnemyDescription description)
        {
            Debug.Assert(!isInitialized, "Enemy: Tried to initialize twice");
            isInitialized = true;

            Description = description;
            Health = new(description.StartHealth);

            Instantiate(description.Skeleton, transform);
        }
    }
}
