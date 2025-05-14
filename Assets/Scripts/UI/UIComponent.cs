using UnityEngine;

namespace TDTest.UI
{
    public abstract class UIComponent : MonoBehaviour
    {
        void Awake()
        {
            Statics.OnInitializationFinish += Initialize;
        }

        void OnDestroy()
        {
            Statics.OnInitializationFinish -= Initialize;
        }

        protected virtual void Initialize() {}
    }
}
