using System.Collections.Generic;
using UnityEngine;

namespace TDTest.Structural
{
    public class GridSystem : ISystem
    {
        public IDictionary<Transform, Structure> TransformStructureLookup => transformStructureLookup;

        Dictionary<Transform, Structure> transformStructureLookup;

        public void Initialize()
        {
            transformStructureLookup = new();
        }

        public void Tick(float deltaTime, float unscaledDeltaTime) { }

        public void Deinitialize()
        {
            transformStructureLookup = null;
        }

        public Grid RegisterStructure(Structure newStructure)
        {
            Debug.Assert(!transformStructureLookup.ContainsKey(newStructure.GridHolder), "GridSystem: Tried to register structure twice.");
            ConfigureGridDescription(newStructure);
            return CreateGridForStructure(newStructure);
        }

        void ConfigureGridDescription(Structure structure)
        {
            if (structure.GridDescription.AutomaticCellSize)
                structure.GridDescription.CalculateCellSize(structure);
        }

        Grid CreateGridForStructure(Structure structure)
        {
            Debug.Assert(structure.GridDescription.IsValid(out var err), err);
            transformStructureLookup.Add(structure.GridHolder, structure);
            return new Grid(structure);
        }
    }
}