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

        void Start()
        {
            Statics.Grid.RegisterStructure(this);
        }

        void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying) return;

            Statics.Grid.StructureGridLookup[this].ForEachCell(DebugDrawSphere);
        }

        void DebugDrawSphere(int x, int y, Cell cell)
        {
            Gizmos.color = (cell.IsOccupied) ? Color.red : Color.green;
            Gizmos.DrawSphere(cell.WorldPosition, .25f);
        }
    }
}
