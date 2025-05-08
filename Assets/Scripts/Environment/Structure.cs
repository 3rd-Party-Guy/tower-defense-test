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
            Gizmos.color = Color.blue;

            var positions = Statics.Grid.StructureGridLookup[this].AllWorldPos();
            positions.ForEach(e => Gizmos.DrawSphere(e, 1f));
        }
    }
}
