using UnityEngine;

namespace TDTest.Structural
{
    public class Cell
    {
        public Vector3 WorldPosition { get; private set; }
        public bool IsOccupied { get; private set; }
    
        public Cell(Vector3 worldPosition, bool isOccupied)
        {
            WorldPosition = worldPosition;
            IsOccupied = isOccupied;
        }

        public void DEBUGOccupyChange()
        {
            IsOccupied = !IsOccupied;
        }
    }
}
