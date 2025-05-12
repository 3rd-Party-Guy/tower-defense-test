using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;

namespace TDTest
{
    [DefaultExecutionOrder(-10)]
    public class GameManager : MonoBehaviour
    {
        void Start()
        {
            Statics.Initialize();

            Statics.Inputs.OnTap += CheckStructureTap;
        }

        private void Update()
        {
            Statics.Tick(UnityEngine.Time.deltaTime, UnityEngine.Time.unscaledTime);
        }

        void OnDestroy()
        {
            Statics.Inputs.OnTap -= CheckStructureTap;

            Statics.Deinitialize();
        }

        void CheckStructureTap(TouchState state)
        {
            var ray = Camera.main.ScreenPointToRay(state.position);
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 10f, false);
            
            if (Physics.Raycast(ray.origin, ray.direction, out var hit, Mathf.Infinity, LayerMask.GetMask("Structure")))
            {
                var structure = Statics.Grids.TransformStructureLookup[hit.transform];
                var grid = structure.Grid;
                var coords = grid.WorldToGrid(hit.point);

                grid.Cells[coords.x, coords.y].FSM.StateMachine.Signal(Structural.CellFSM.Trigger.ToBuilding);
            }
        }
    }
}