using TDTest.Structure;
using UnityEngine;

namespace TDTest.Structural
{
    public class Cell
    {
        public Vector3 WorldPosition { get; private set; }
        public CellFSM FSM { get; private set; }
    
        public Cell(Vector3 worldPosition)
        {
            WorldPosition = worldPosition;
            FSM = new();
        }
    }
}
