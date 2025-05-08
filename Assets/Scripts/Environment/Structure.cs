using UnityEngine;

namespace TDTest.Structural
{
    /// <summary>
    /// Type: MonoBehvaiour
    /// Description: Defines a surface eligible for a grid
    /// </summary>
    public class Structure : MonoBehaviour
    {
        [field: SerializeField] public Transform GridHolder { get; private set; }
        [field: SerializeField] public GridDescription GridDescription { get; private set; }

        public Vector3 WorldStartPos { get; private set; }

        void Start()
        {
            Statics.Grid.RegisterStructure(this);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;

            var centerPos = GridHolder.position;

            // planes are 10x10, this is lower left before scale
            var localLowerLeft = new Vector3(-5f, 0f, -5f);
            var worldStartPos = Vector3.Scale(localLowerLeft, GridHolder.localScale);

            WorldStartPos = worldStartPos;
            Gizmos.DrawSphere(worldStartPos, 1f);
        }
    }
}
