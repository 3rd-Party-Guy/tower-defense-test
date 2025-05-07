using System.Collections.Generic;
using UnityEngine;

namespace TDTest.Grid
{
    public class GridSystem : ISystem
    {
        List<Structure> structures;

        public void Initialize()
        {
            structures = new();
        }

        public void Tick(float deltaTime, float unscaledDeltaTime) { }

        public void Deinitialize()
        {
            structures = null;
        }

        public void RegisterStructure(Structure newStructure)
        {
            Debug.Assert(!structures.Contains(newStructure), "GridSystem: Tried to register structure twice.");

            structures.Add(newStructure);
            CreateGridForStructure(newStructure);
        }

        void CreateGridForStructure(Structure structure)
        {

        }
    }
}