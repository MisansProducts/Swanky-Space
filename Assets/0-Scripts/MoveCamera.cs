using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform cameraPosition;
    [SerializeField] float smoothTime = 0.3f;

    Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition.position, ref velocity, smoothTime);
    }
}
