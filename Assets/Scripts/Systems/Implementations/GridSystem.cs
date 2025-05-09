using System.Collections.Generic;
using UnityEngine;

namespace TDTest.Structural
{
    public class GridSystem : ISystem
    {
        public IDictionary<Structure, Grid> StructureGridLookup => structureGridLookup;

        Dictionary<Structure, Grid> structureGridLookup;

        public void Initialize()
        {
            structureGridLookup = new();
        }

        public void Tick(float deltaTime, float unscaledDeltaTime) { }

        public void Deinitialize()
        {
            structureGridLookup = null;
        }

        public void RegisterStructure(Structure newStructure)
        {
            Debug.Assert(!structureGridLookup.ContainsKey(newStructure), "GridSystem: Tried to register structure twice.");
            ConfigureGridDescription(newStructure);
            CreateGridForStructure(newStructure);
        }

        public Vector3 GridToWorldPos(Structure structure, int x, int y)
        {
            Debug.Assert(structureGridLookup[structure] != null, "GridSystem: Tried to calculate world position for unregistered grid.");
            return structureGridLookup[structure].ToWorldPos(x, y);
        }

        void ConfigureGridDescription(Structure structure)
        {
            if (structure.GridDescription.AutomaticCellSize)
                structure.GridDescription.CalculateCellSize(structure);
        }

        void CreateGridForStructure(Structure structure)
        {
            Debug.Assert(structure.GridDescription.IsValid(out var err), err);
            structureGridLookup.Add(structure, new Grid(structure));
        }
    }
}