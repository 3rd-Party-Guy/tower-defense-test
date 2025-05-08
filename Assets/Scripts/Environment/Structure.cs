using UnityEngine;

namespace TDTest.Structural
{
    /// <summary>
    /// Type: MonoBehvaiour
    /// Description: Defines a surface eligible for a grid
    /// </summary>
    public class Structure : MonoBehaviour
    {
        [field: SerializeField] public Transform gridHolder { get; private set; }
        [field: SerializeField] public GridDescription GridDescription { get; private set; }

        void Start()
        {
            Statics.Grid.RegisterStructure(this);
        }
    }
}
