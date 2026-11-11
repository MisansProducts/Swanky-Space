using UnityEngine;

public class Player : MonoBehaviour
{
    float oX;
    float oY;
    float oZ;
    [SerializeField] float rotationSpeed = 1f;
    
    void Start()
    {
        oX = transform.eulerAngles.x;
        oY = transform.eulerAngles.y;
        oZ = transform.eulerAngles.z;
    }
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = Camera.main.nearClipPlane;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        float angle = Mathf.Atan2(worldPos.y, worldPos.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(oX + angle, oY, oZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
