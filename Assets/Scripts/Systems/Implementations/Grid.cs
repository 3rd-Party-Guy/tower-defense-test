using UnityEngine;

namespace TDTest.Structural
{
    public class Grid
    {
        Cell[,] cells;
        GridDescription description;

        public Grid(GridDescription description)
        {
            this.description = description;
            cells = new Cell[description.Width, description.Height];
        }

        public void ToWorldPos(int x, int y)
        {
            Debug.Assert(x >= 0 && x < description.Width, $"Grid: Tried to get world position for invalid x Coordinate ({x})");
            Debug.Assert(y >= 0 && y < description.Height, $"Grid: Tried to get world position for invalid y Coordinate ({y})");


        }
    }
}
