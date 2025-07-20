using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace HunterGatherer.PlayerInput
{
    public class MouseInformation : MonoBehaviour
    {
        public delegate void MouseDown(object sender, EventArgs e);
        public delegate void MouseUp(object sender, EventArgs e);
        public static event MouseDown MouseDownPerformed;
        public static event MouseUp MouseUpPerformed;

        public static Vector2 mousePosition { get; private set; }

        private static Camera cam;

        private void Awake()
        {
            cam = Camera.main;
        }

        public static void MousePosition(InputAction.CallbackContext context)
        {
            mousePosition = context.ReadValue<Vector2>();
        }

        public void OnMouseClick(InputAction.CallbackContext context)
        {
            if (KeyboardInformation.anyKey) return;

            if (context.performed)
            {
                MouseDownPerformed?.Invoke(this, EventArgs.Empty);
            }
            else if (context.canceled)
            {
                MouseUpPerformed?.Invoke(this, EventArgs.Empty);
            }
        }

        public static Vector3Int CellFromMouse(Tilemap tilemap)
        {
            var worldPoint = cam.ScreenToWorldPoint(mousePosition);
            worldPoint.z = tilemap.transform.position.z;
            return tilemap.WorldToCell(worldPoint);
        }
    }
}