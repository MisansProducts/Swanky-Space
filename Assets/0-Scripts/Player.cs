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

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Smooth rotation
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(-Vector3.forward, transform.position);
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 worldPos = ray.GetPoint(distance);
            Vector3 direction = worldPos - transform.position;
            direction.z = 0f;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg % 360;
            if (angle < 0) angle += 360;
            Quaternion targetRotation = Quaternion.Euler(angle, oY, oZ);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        // Acceleration using semi-implicit Euler integration
        if (Input.GetKey(thrustKey))
            velocity += acceleration * -transform.forward * Time.deltaTime;
        velocity *= Mathf.Exp(-drag * Time.deltaTime); // drag
        velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
        transform.position += velocity * Time.deltaTime;
    }
}
