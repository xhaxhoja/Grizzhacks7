using UnityEngine;

public class HostileMovement : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private Camera mainCamera;
    private bool hasCrossedViewOnce = false;

    // Initialize with direction, speed, and camera reference
    public void Initialize(Vector3 moveDirection, float moveSpeed, Camera camera)
    {
        speed = moveSpeed;
        mainCamera = camera;

        // Ensure the object moves towards the camera's visible area
        direction = (mainCamera.transform.position - transform.position).normalized; // Move towards the camera
    }

    private void Update()
    {
        // Move the hostile object in the set direction
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if the object has crossed the camera's bounds
        if (IsOutsideCameraView() && !hasCrossedViewOnce)
        {
            hasCrossedViewOnce = true; // Mark it as crossed once
        }
        else if (IsOutsideCameraView() && hasCrossedViewOnce)
        {
            // Destroy the object when it leaves the camera view a second time
            Destroy(gameObject);
        }
    }

    private bool IsOutsideCameraView()
    {
        // Convert world position to viewport position (0 to 1 range for x and y, 0 to 1 for z)
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

        // Check if the object is outside the camera bounds
        return viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1;
    }
}
