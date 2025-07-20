using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using HunterGatherer.PlayerInput;

namespace HunterGatherer.UI
{
    public class Highlighting : MonoBehaviour
    {
        public Tilemap terrain;
        public Color mouseOverColor;
        public Color mouseDownColor;
        private Color defaultColor = Color.white;

        private Vector3Int prevCell;
        private Vector3Int currentCell;
        private Tile[] tiles;


        private void Awake()
        {
            currentCell = Vector3Int.zero;
            prevCell = Vector3Int.zero;
        }
        
        private void OnEnable()
        {
            MouseInformation.MouseDownPerformed += OnMouseDown;
            MouseInformation.MouseUpPerformed += OnMouseUp;
        }

        private void OnDisable()
        {
            MouseInformation.MouseDownPerformed -= OnMouseDown;
            MouseInformation.MouseUpPerformed -= OnMouseUp;
        }

        private void Update()
        {
            currentCell = MouseInformation.CellFromMouse(terrain);
            if (currentCell != prevCell)
            {
                Highlight(prevCell,defaultColor);
                Highlight(currentCell, mouseOverColor);
                prevCell = MouseInformation.CellFromMouse(terrain);
            }
        }

        private void Highlight(Vector3Int cell, Color color)
        {
            terrain.SetColor(cell, color);
            terrain.RefreshTile(cell);
        }

        public void OnMouseDown(object sender, EventArgs e)
        {
            Highlight(currentCell, mouseDownColor);
        }

        public void OnMouseUp(object sender, EventArgs e)
        {
            Highlight(currentCell, mouseOverColor);
        }

    }
}