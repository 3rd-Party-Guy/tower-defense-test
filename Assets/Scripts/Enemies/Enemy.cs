using TDTest.Structural;
using UnityEngine;

namespace TDTest.Combat
{
    public class Enemy : MonoBehaviour
    {
        public Health Health { get; private set; }
        public EnemyDescription Description { get; private set; }
        public Structure Structure { get; private set; }
        bool isInitialized = false;

        public void Initialize(EnemyDescription description, Structure structure)
        {
            Debug.Assert(!isInitialized, "Enemy: Tried to initialize twice");
            isInitialized = true;

            Description = description;
            Health = new(description.StartHealth);
            Structure = structure;

            Instantiate(description.Skeleton, transform);
        }
    }
}
