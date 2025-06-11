using UnityEngine;

public class FollowSignUI : MonoBehaviour
{
    public Transform sign;              // objek papan sign di dunia
    public Vector3 screenOffset;        // offset layar (misalnya ke atas 50px)

    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(sign.position);
        transform.position = screenPos + screenOffset;
    }
}
