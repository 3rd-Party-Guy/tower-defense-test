using UnityEngine;

namespace TDTest.Structural
{
    /// <summary>
    /// Type: Description
    /// Description: Information about a grid
    /// </summary>
    [System.Serializable]
    public struct GridDescription
    {
        public int Height => height;
        public int Width => width;
        public float CellSize => cellSize;

        [SerializeField] int height;
        [SerializeField] int width;
        [SerializeField] float cellSize;

        public bool IsValid(out string errorMessage)
        {
            if (Height < 5)
            {
                errorMessage = "GridDescription: Must have a height of 5 or more.";
                return false;
            }

            if (Width < 5)
            {
                errorMessage = "GridDescription: Must have a width of 5 or more.";
                return false;
            }

            if (CellSize <= 0f)
            {
                errorMessage = "GridDescription: Must have a cell size above 0f";
                return false;
            }

            errorMessage = "";
            return true;
        }
    }
}
