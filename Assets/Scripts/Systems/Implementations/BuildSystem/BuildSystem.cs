using System;
using System.Collections.Generic;
using System.Linq;
using TDTest.Combat;
using TDTest.Structural;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace TDTest.Building
{
    public class BuildSystem : ISystem
    {
        public Action OnCanPlaceChange;
        public Action OnAvailableTurretsChange;
        public Action OnPreviewChange;

        public BuildFSM FSM { get; private set; }
        public IEnumerable<TurretDescription> AvailableTurrets => availableTurrets;

        public bool CanPlace
        {
            get => canPlace;
            set
            {
                canPlace = value;
                OnCanPlaceChange?.Invoke();
            }
        }
        bool canPlace;

        List<TurretDescription> availableTurrets;
        Turret turretPrefab;
        PreviewTurret previewTurretPrefab;

        PreviewTurret previewTurret;

        TurretDescription selectedDescription;
        Structural.Grid selectedGrid;
        Vector2Int selectedCoords;

        public void Initialize()
        {
            FSM = new();
            previewTurret = null;
        }

        public void Tick(float deltaTime, float unscaledDeltaTime)
        {
        }

        public void Deinitialize()
        {
        }

        public void SetAvailableTurrets(List<TurretDescription> turrets)
        {
            availableTurrets = turrets;
            OnAvailableTurretsChange?.Invoke();
        }

        public void SetPreviewTurretPrefab(PreviewTurret newPreviewTurret)
        {
            previewTurretPrefab = newPreviewTurret;
        }

        public void SetTurretPrefab(Turret newTurretPrefab)
        {
            turretPrefab = newTurretPrefab;
        }

        public void EnterMove(TurretDescription description)
        {
            selectedDescription = description;
            Statics.Inputs.OnTouchBegan += PreviewTurret;
            FSM.StateMachine.Signal(BuildFSM.Trigger.StartMove);

            previewTurret = null;
        }

        public void EnterBuild(TurretDescription description)
        {
            selectedDescription = description;
            Statics.Inputs.OnTouchBegan += PreviewTurret;
            FSM.StateMachine.Signal(BuildFSM.Trigger.StartBuild);

            previewTurret = null;
        }

        public void ExitMove()
        {
            Statics.Inputs.OnTouchBegan -= PreviewTurret;
            FSM.StateMachine.Signal(BuildFSM.Trigger.CancelMove);

            previewTurret = null;
        }

        public void ExitBuild()
        {
            Statics.Inputs.OnTouchBegan -= PreviewTurret;
            FSM.StateMachine.Signal(BuildFSM.Trigger.CancelBuild);

            previewTurret = null;
        }

        public bool CanPlaceOnGrid()
        {
            var cellsToOccupy = new List<Cell>();
            selectedDescription.GridOffsets.ForEach(e =>
            {
                var coord = new Vector2Int(selectedCoords.x + e.x, selectedCoords.y + e.y);
                var cell = selectedGrid.Cells[coord.x, coord.y];
                cellsToOccupy.Add(cell);
            });

            return cellsToOccupy.All(e => e.FSM.StateMachine.State is CellFSM.State.Free);
        }

        public void PlaceTurretOnGrid(bool takeMoney)
        {
            var cellsToOccupy = new List<Cell>();
            selectedDescription.GridOffsets.ForEach(e =>
            {
                var coord = new Vector2Int(selectedCoords.x + e.x, selectedCoords.y + e.y);
                var cell = selectedGrid.Cells[coord.x, coord.y];
                cellsToOccupy.Add(cell);
            });

            cellsToOccupy.ForEach(e =>
            {
                e.FSM.StateMachine.Signal(CellFSM.Trigger.ToBuilding);
            });

            var turret = UnityEngine.Object.Instantiate(turretPrefab);
            turret.Initialize(selectedDescription);
            turret.transform.position = selectedGrid.Cells[selectedCoords.x, selectedCoords.y].WorldPosition;

            if (takeMoney)
            {
                Statics.Gold.RemoveGold(selectedDescription.Cost);
            }

            if (previewTurret != null)
                UnityEngine.Object.Destroy(previewTurret.gameObject);

            if (FSM.StateMachine.State is BuildFSM.State.Build)
                ExitBuild();
            else if (FSM.StateMachine.State is BuildFSM.State.Move)
                ExitMove();
        }

        void PreviewTurret(TouchState state)
        {
            var ray = Camera.main.ScreenPointToRay(state.position);
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 10f, false);

            if (Physics.Raycast(ray.origin, ray.direction, out var hit, Mathf.Infinity, LayerMask.GetMask("Structure")))
            {
                var structure = Statics.Grids.TransformStructureLookup[hit.transform];
                var grid = structure.Grid;
                var coords = grid.WorldToGrid(hit.point);

                selectedGrid = grid;
                selectedCoords = coords;

                if (previewTurret == null)
                {
                    previewTurret = UnityEngine.Object.Instantiate(previewTurretPrefab);
                    previewTurret.Initialize(selectedDescription);
                }

                previewTurret.transform.position = grid.Cells[coords.x, coords.y].WorldPosition;

                OnPreviewChange?.Invoke();
            }
        }
    }
}
