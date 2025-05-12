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
            var grid = CreateGridForStructure(newStructure);
            CreateGridEnemyPath(grid, newStructure.EnemyPath);
            return grid;
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

        void CreateGridEnemyPath(Grid grid, List<Vector2Int> path)
        {
            bool IsPathInBounds(int x, int y)
            {
                var xInBounds = (x >= 0 || x < grid.Cells.GetLength(0));
                var yInBounds = (x >= 0 || x < grid.Cells.GetLength(1));

                return xInBounds && yInBounds;
            }

            path.ForEach(e =>
            {
                if (!IsPathInBounds(e.x, e.y))
                {
                    Debug.LogError($"GridSystem: Tried to create enemy path outside of bounds ({e.x}, {e.y}");
                    return;
                }

                grid.Cells[e.x, e.y].FSM.StateMachine.Signal(CellFSM.Trigger.ToEnemyPath);
            });
        }
    }
}