using UnityEngine;

namespace TDTest.Structural
{
    public class Grid
    {
        Cell[,] cells;
        GridDescription description;
        Vector3 worldOrigin;

        public Grid(Structure structure)
        {
            this.description = structure.GridDescription;
            cells = new Cell[description.Width, description.Height];

            CalculateWorldOrigin(structure);
        }

        public Vector3 ToWorldPos(int x, int y)
        {
            Debug.Assert(x >= 0 && x < description.Width, $"Grid: Tried to get world position for invalid x Coordinate ({x})");
            Debug.Assert(y >= 0 && y < description.Height, $"Grid: Tried to get world position for invalid y Coordinate ({y})");

            var cellSize = description.CellSize;

            return new Vector3(
                worldOrigin.x + cellSize * x,
                worldOrigin.y,
                worldOrigin.z + cellSize * y);
        }

        void CalculateWorldOrigin(Structure structure)
        {
            var gridHolder = structure.GridHolder;
            var centerPos = gridHolder.position;
            var localLowerLeft = new Vector3(-5f, 0f, -5f);
            var worldStartPos = Vector3.Scale(localLowerLeft, gridHolder.localScale);

            worldOrigin = worldStartPos;
        }
    }
}
