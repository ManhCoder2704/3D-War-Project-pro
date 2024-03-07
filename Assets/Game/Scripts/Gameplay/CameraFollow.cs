using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // The target GameObject that the camera will follow
    public Vector3 offset;    // The offset distance between the camera and the target
    public float smoothSpeed = 0.125f;  // The smoothness of the camera follow movement

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position of the camera based on the target's position and offset
            Vector3 desiredPosition = target.position + offset;

            // Smoothly move the camera towards the desired position using SmoothDamp
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update the camera's position
            transform.position = smoothedPosition;

            // Make the camera look at the target
            transform.LookAt(target);
        }
    }
}
