using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;

namespace TDTest.Input
{
    public class InputHandleSystem : ISystem
    {
        public Action<TouchState> OnTouchAny;
        public Action<TouchState> OnTouchBegan;
        public Action<TouchState> OnTouchMoved;
        public Action<TouchState> OnTouchEnded;

        InputAction tapAction;

        public void Initialize()
        {
            if (Application.isPlaying && IsPlatformEditor())
            {
                Debug.Log("Simulating Touchscreen...");
                TouchSimulation.Enable();
            }

            tapAction = InputSystem.actions.FindActionMap("Player").FindAction("Tap");
            tapAction.performed += OnTouched;
            tapAction.Enable();
        }

        public void Tick(float deltaTime, float unscaledDeltaTime) { }
        
        public void Deinitialize()
        {
            if (Application.isPlaying && IsPlatformEditor())
            {
                TouchSimulation.Disable();
            }

            tapAction.performed -= OnTouched;
            tapAction.Disable();
        }

        void OnTouched(InputAction.CallbackContext context)
        {
            var touchState = context.ReadValue<TouchState>();

            OnTouchAny?.Invoke(touchState);

            switch (touchState.phase)
            {
                case UnityEngine.InputSystem.TouchPhase.Began:
                    OnTouchBegan?.Invoke(touchState);
                    break;
                case UnityEngine.InputSystem.TouchPhase.Moved:
                    OnTouchMoved?.Invoke(touchState);
                    break;
                case UnityEngine.InputSystem.TouchPhase.Ended:
                    OnTouchEnded?.Invoke(touchState);
                    break;
                default:
                    break;
            }
        }

        bool IsPlatformEditor()
        {
            return Application.platform == RuntimePlatform.WindowsEditor
                || Application.platform == RuntimePlatform.OSXEditor
                || Application.platform == RuntimePlatform.LinuxEditor;
        }

    }
}
