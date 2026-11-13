using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    float oY;
    float oZ;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] KeyCode thrustKey = KeyCode.W;
    [SerializeField] float acceleration = 1f;
    Vector3 velocity = Vector3.zero;
    [SerializeField] float drag = 1f;
    [SerializeField] float maxVelocity = 1f;
    
    void Start()
    {
        oY = transform.eulerAngles.y;
        oZ = transform.eulerAngles.z;
    }
    void Update()
    {
        Transform cameraTransform = Camera.main.GetComponent<Transform>();
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y, cameraTransform.position.z);
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Smooth rotation
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = Camera.main.nearClipPlane;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.x -= transform.position.x;
        worldPos.y -= transform.position.y;
        float angle = Mathf.Atan2(worldPos.y, worldPos.x) * Mathf.Rad2Deg % 360;
        if (angle < 0) angle += 360;
        Quaternion targetRotation = Quaternion.Euler(angle, oY, oZ);
        transform.rotation = Quaternion.Euler(Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime).eulerAngles);

        // Acceleration using semi-implicit Euler integration
        if (Input.GetKey(thrustKey))
            velocity += acceleration * -transform.forward * Time.deltaTime;
        velocity *= Mathf.Exp(-drag * Time.deltaTime); // drag
        velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
        transform.position += velocity * Time.deltaTime;
    }
}
