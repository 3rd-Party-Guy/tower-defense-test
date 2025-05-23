using System.Collections.Generic;
using TDTest.Combat;
using UnityEngine;

namespace TDTest.Structural
{
    public class Structure : MonoBehaviour
    {
        public Grid Grid { get; private set; }

        [field: SerializeField] public Transform GridHolder { get; private set; }
        [field: SerializeField] public GridDescription GridDescription { get; private set; }
        [field: SerializeField] public List<Vector2Int> EnemyPath { get; private set; }
        [field: SerializeField] public List<EnemyWaveDescription> EnemyWaveDescriptions { get; private set; }

        void Start()
        {
            Grid = Statics.Grids.RegisterStructure(this);
            Statics.Combat.RegisterStructure(this);
        }
    }
}
