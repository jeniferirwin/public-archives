using UnityEngine;

namespace HunterGatherer.PlayerInput
{
    public class Panning : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed;
        private Vector3 moveInput;

        private void Update()
        {
            moveInput = Vector3.zero;
            if (!KeyboardInformation.anyKey) return;

            if (KeyboardInformation.leftPressed)
            {
                moveInput.x = -1;
            }
            if (KeyboardInformation.rightPressed)
            {
                moveInput.x = 1;
            }
            if (KeyboardInformation.upPressed)
            {
                moveInput.y = 1;
            }
            if (KeyboardInformation.downPressed)
            {
                moveInput.y = -1;
            }
            var target = transform.position + (moveInput * moveSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, target, (moveSpeed * Time.deltaTime));
        }
    }
}