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

        public List<Vector3> GridPathToWorldPath(Grid grid, List<Vector2Int> gridPath)
        {
            var worldPath = new List<Vector3>();

            gridPath.ForEach(e =>
            {
                Debug.Assert(AreCoordsInBounds(grid, e.x, e.y), $"GridSystem: Tried to create enemy path outside of bounds ({e.x}, {e.y}");
                var worldPos = grid.Cells[e.x, e.y].WorldPosition;
                worldPath.Add(worldPos);
            });

            return worldPath;
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
            path.ForEach(e =>
            {
                Debug.Assert(AreCoordsInBounds(grid, e.x, e.y), $"GridSystem: Tried to create enemy path outside of bounds ({e.x}, {e.y}");
                grid.Cells[e.x, e.y].FSM.StateMachine.Signal(CellFSM.Trigger.ToEnemyPath);
            });
        }

        bool AreCoordsInBounds(Grid grid, int x, int y)
        {
            var xInBounds = (x >= 0 && x < grid.Cells.GetLength(0));
            var yInBounds = (y >= 0 && y < grid.Cells.GetLength(1));

            return xInBounds && yInBounds;
        }
    }
}