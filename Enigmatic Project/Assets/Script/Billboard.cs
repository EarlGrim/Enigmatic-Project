using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera mainCamera; // Assign this manually in the Inspector

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Try to find the camera automatically
        }

        if (mainCamera == null)
        {
            Debug.LogError("❌ No Main Camera found! Assign a camera manually in the Inspector.");
        }
    }

    void LateUpdate()
    {
        if (mainCamera == null) return; // Prevent errors if the camera is missing

        Vector3 cameraPosition = mainCamera.transform.position;
        cameraPosition.y = transform.position.y; // Lock Y rotation
        transform.LookAt(cameraPosition);
    }
}
