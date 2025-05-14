using TDTest.Structural;
using UnityEngine;

namespace TDTest.Combat
{
    public class Enemy : MonoBehaviour
    {
        public int PathIndex { get; set; }
        public Health Health { get; private set; }
        public EnemyDescription Description { get; private set; }
        public Structure Structure { get; private set; }

        public void Initialize(EnemyDescription description, Structure structure)
        {
            Description = description;
            Health = new(description.StartHealth);
            Structure = structure;

            Instantiate(description.Skeleton, transform);
        }

        public void SetPosition(Vector2Int gridCoordinates)
        {
            var worldPosition = Structure.Grid.Cells[gridCoordinates.x, gridCoordinates.y].WorldPosition;
            transform.position = worldPosition;
        }
    }
}
