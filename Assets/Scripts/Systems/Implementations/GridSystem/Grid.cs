using System;
using System.Collections.Generic;
using UnityEngine;

namespace TDTest.Structural
{
    public class Grid
    {
        public Cell[,] Cells { get; private set; }

        GridDescription description;
        Vector3 worldOrigin;

        public Grid(Structure structure)
        {
            this.description = structure.GridDescription;

            CalculateWorldOrigin(structure);
            InitializeCellsArray();
        }

        public List<Vector3> AllWorldPos()
        {
            var positions = new List<Vector3>();

            for (int x = 0; x < description.Width; x++)
            for (int y = 0; y < description.Height; y++)
                positions.Add(CalculateCellWorldPosition(x, y));

            return positions;
        }

        public Vector2Int WorldToGrid(Vector3 worldPos)
        {
            var gridPos = new Vector2(worldPos.x - worldOrigin.x, worldPos.z - worldOrigin.z);
            var x = Mathf.FloorToInt(gridPos.x / description.CellSize);
            var y = Mathf.FloorToInt(gridPos.y / description.CellSize);

            return new Vector2Int(x, y);
        }

        public void ForEachCell(Action<int, int, Cell> func)
        {
            for (int x = 0; x < description.Width; x++)
            for (int y = 0; y < description.Height; y++)
                func(x, y, Cells[x, y]);
        }

        void InitializeCellsArray()
        {
            Cells = new Cell[description.Width, description.Height];
            ForEachCell((x, y, _) => Cells[x, y] = new(CalculateCellWorldPosition(x, y)));
        }

        Vector3 CalculateCellWorldPosition(int x, int y)
        {
            Debug.Assert(x >= 0 && x < description.Width, $"Grid: Tried to get world position for invalid x Coordinate ({x})");
            Debug.Assert(y >= 0 && y < description.Height, $"Grid: Tried to get world position for invalid y Coordinate ({y})");

            var cellSize = description.CellSize;

            return new Vector3(
                worldOrigin.x + cellSize * x + cellSize * 0.5f,
                worldOrigin.y,
                worldOrigin.z + cellSize * y + cellSize * 0.5f);
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
