using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 2f;
    public float sensitivity = 20.0f;

    private float rotationX = 0f;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.rotation = target.rotation;
    }
    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.RotateAround(target.position, Vector3.up, mouseX);
        transform.rotation = Quaternion.Euler(rotationX, transform.rotation.eulerAngles.y, 0f);
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
