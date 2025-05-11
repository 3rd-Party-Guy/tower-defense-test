using TDTest.Structural;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;

namespace TDTest
{
    [DefaultExecutionOrder(-10)]
    public class GameManager : MonoBehaviour
    {
        InputAction tapAction;

        void Awake()
        {
            // simulate touchscreen
            if (Application.isPlaying && Application.platform == RuntimePlatform.WindowsEditor)
            {
                Debug.Log("Simulating Touchscreen...");
                TouchSimulation.Enable();
            }
        }

        void Start()
        {

            Statics.Initialize();

            tapAction = InputSystem.actions.FindActionMap("Player").FindAction("Tap");
            tapAction.performed += DEBUGHandleTouch;
            tapAction.Enable();
        }

        private void Update()
        {
            Statics.Tick(UnityEngine.Time.deltaTime, UnityEngine.Time.unscaledTime);
        }

        void OnDestroy()
        {
            Statics.Deinitialize();

            if (!Application.isPlaying || Application.platform == RuntimePlatform.WindowsEditor)
            {
                TouchSimulation.Disable();
            }

            tapAction.performed -= DEBUGHandleTouch;
            tapAction.Disable();
        }

        void DEBUGHandleTouch(InputAction.CallbackContext ctx)
        {
            var touchState = ctx.ReadValue<TouchState>();
            CheckStructureTap(touchState);
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

                grid.Cells[coords.x, coords.y].DEBUGOccupyChange();
            }
        }
    }
}