using UnityEngine;

namespace TDTest
{
    [DefaultExecutionOrder(-10)]
    public class GameManager : MonoBehaviour
    {
        void Start()
        {
            Statics.Initialize();
        }

        private void Update()
        {
            Statics.Tick(UnityEngine.Time.deltaTime, UnityEngine.Time.unscaledTime);
        }

        void OnDestroy()
        {
            Statics.Deinitialize();
        }
    }
}