using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;

namespace TDTest.Input
{
    public class InputHandleSystem : ISystem
    {
        public Action<TouchState> OnTap;

        InputAction tapAction;

        public void Initialize()
        {
            if (Application.isPlaying && IsPlatformEditor())
            {
                Debug.Log("Simulating Touchscreen...");
                TouchSimulation.Enable();
            }

            tapAction = InputSystem.actions.FindActionMap("Player").FindAction("Tap");
            tapAction.performed += OnTapped;
            tapAction.Enable();
        }

        public void Tick(float deltaTime, float unscaledDeltaTime) { }
        
        public void Deinitialize()
        {
            if (Application.isPlaying && IsPlatformEditor())
            {
                TouchSimulation.Disable();
            }

            tapAction.performed -= OnTapped;
            tapAction.Disable();
        }

        bool IsPlatformEditor()
        {
            return Application.platform == RuntimePlatform.WindowsEditor
                || Application.platform == RuntimePlatform.OSXEditor
                || Application.platform == RuntimePlatform.LinuxEditor;
        }

        void OnTapped(InputAction.CallbackContext context)
        {
            var touchState = context.ReadValue<TouchState>();
            OnTap?.Invoke(touchState);
        }
    }
}
