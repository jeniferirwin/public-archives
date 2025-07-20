using UnityEngine;
using UnityEngine.InputSystem;
using Shooty.Core;

namespace Shooty.Gameplay
{
    public class Shoot : MonoBehaviour
    {
        private Camera mainCamera;
        private Vector2 mousePos;

        private void Start()
        {
            mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (!context.started) return;
            var ray = mainCamera.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Target target;
                if (hit.transform.gameObject.TryGetComponent<Target>(out target))
                {
                    if (!hit.transform.gameObject.CompareTag("Target")) return;
                    target.GetShot();
                }
            }
        }

        public void SetMousePosition(InputAction.CallbackContext context)
        {
            mousePos = context.ReadValue<Vector2>();
        }
    }
}
