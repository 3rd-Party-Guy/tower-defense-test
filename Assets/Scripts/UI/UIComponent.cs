using UnityEngine;

namespace TDTest.UI
{
    public abstract class UIComponent : MonoBehaviour
    {
        void Awake()
        {
            Statics.OnInitializationFinish += Initialize;
        }

        private void OnDestroy()
        {
            Statics.OnInitializationFinish -= Initialize;
        }

        protected virtual void Initialize() {}
    }
}
