using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace HunterGatherer.UI
{
    public class Manipulation : MonoBehaviour
    {
        [SerializeField] private Tilemap units;
        [SerializeField] private Tile camp;
        
        private Vector3Int prevCell;
        private Vector3Int currentCell;
        
        private Tile currentBuild;
        private bool canPlace;
        
        private bool clickBuffered;
        
        private void Start()
        {
            prevCell = Vector3Int.zero;
            currentCell = Vector3Int.zero;
            canPlace = true;
            currentBuild = camp;
            ObserveClicks(true);
        }

        private void Update()
        {
            currentCell = PlayerInput.MouseInformation.CellFromMouse(units);
            HandlePositionChanges();
            HandleClicks();
        }
        
        private void HandlePositionChanges()
        {
            if (currentCell != prevCell)
            {
                // if canPlace was active at the time we left the previous
                // cell, that means the previous cell wasn't occupied by
                // anything, so we can safely clear it
                if (canPlace)
                {
                    units.SetTile(prevCell,null);
                    units.RefreshTile(prevCell);
                }

                if (!units.HasTile(currentCell))
                {
                    canPlace = true;
                    units.SetTile(currentCell,camp);
                    units.RefreshTile(currentCell);
                }
                else
                {
                    canPlace = false;
                }
                prevCell = currentCell;
            }
        }
        
        private void HandleClicks()
        {
            if (!clickBuffered) return;
            clickBuffered = false;
            if (canPlace == true && currentBuild != null)
            {
                canPlace = false;
                currentBuild = null;
            }
        }
        
        private void BufferClick(object sender, EventArgs e)
        {
            clickBuffered = true;
        }
        
        private void ClearCell(Vector3Int cell)
        {
            units.SetTile(cell, null);
            units.RefreshTile(cell);
        }

        private void ObserveClicks(bool value)
        {
            if (value)
                PlayerInput.MouseInformation.MouseUpPerformed += BufferClick;
            else
                PlayerInput.MouseInformation.MouseUpPerformed -= BufferClick;
        }
    }
}