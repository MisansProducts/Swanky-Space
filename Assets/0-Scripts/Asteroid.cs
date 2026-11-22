using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Vector3 direction;
    Vector3 rotationAxis;

    [SerializeField] float speed = 1f;
    [SerializeField] float radius = 1f;

    void Start()
    {
        direction = Random.insideUnitSphere;
        direction.z = 0f;
        direction.Normalize();
        rotationAxis = Vector3.Cross(Vector3.back, direction).normalized;
    }

    void FixedUpdate()
    {
        // Linear velocity
        transform.position += direction * speed * Time.deltaTime;
        
        // Angular velocity
        float angularVelocity = speed / radius * Mathf.Rad2Deg;
        Quaternion deltaRotation = Quaternion.AngleAxis(angularVelocity * Time.deltaTime, rotationAxis);
        transform.rotation = deltaRotation * transform.rotation;
    }
}
