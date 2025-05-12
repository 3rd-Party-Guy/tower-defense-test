using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace TDTest.Structural
{
    public class Structure : MonoBehaviour
    {
        public Grid Grid { get; private set; }

        [field: SerializeField] public Transform GridHolder { get; private set; }
        [field: SerializeField] public GridDescription GridDescription { get; private set; }

        void Start()
        {
            Grid = Statics.Grids.RegisterStructure(this);
        }

        void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying) return;
            Grid.ForEachCell(DebugDrawSphere);
        }

        void DebugDrawSphere(int x, int y, Cell cell)
        {
            Gizmos.color = (cell.IsOccupied) ? Color.red : Color.green;
            Gizmos.DrawSphere(cell.WorldPosition, .25f);
        }
    }
}
