using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;   // Transform karakter
    public float smoothSpeed = 0.125f;   // Kecepatan smoothing
    public Vector3 offset;     // Offset posisi kamera

    void LateUpdate()
    {
        // Posisi yang diinginkan kamera
        Vector3 desiredPosition = new Vector3(player.position.x, player.position.y, transform.position.z) + offset;

        // Lakukan smoothing
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update posisi kamera
        transform.position = smoothedPosition;
    }
}
