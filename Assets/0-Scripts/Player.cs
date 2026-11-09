using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    float oX;
    float oY;
    float oZ;
    // [SerializeField] float speed = 1f;
    // float t = 0f;
    // float startX = 0f;
    // float endX = 0f;
    void Start()
    {
        oX = transform.eulerAngles.x;
        oY = transform.eulerAngles.y;
        oZ = transform.eulerAngles.z;
    }
    void Update()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = Camera.main.nearClipPlane;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        // Debug.Log(worldPos);
        // startX = transform.eulerAngles.x;
        float angle = (float)(Math.Atan2(worldPos.y, worldPos.x) * (180 / Math.PI));
        // endX = oX + angle;
        // t += Time.deltaTime * speed;
        // t = Mathf.Clamp01(t);
        // Debug.Log(t);
        transform.rotation = Quaternion.Euler(new Vector3(oX + angle, oY, oZ));
    }
}
