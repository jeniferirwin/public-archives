using UnityEngine;

public class MouseSpotlight : MonoBehaviour
{
    public GameObject spotlight;
    public LayerMask groundMask;

    void FixedUpdate()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 30, groundMask))
        {
            transform.LookAt(hit.point);
        }
    }
}
