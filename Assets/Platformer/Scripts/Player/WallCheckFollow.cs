using UnityEngine;

public class WallCheckFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float baseOffset = 0.8f;
    public float dashOffset = 1.2f;
    public Animator playerAnimator;
    public bool isFacingRight = true; // default ke kanan

    void LateUpdate()
    {
        if (playerTransform == null || playerAnimator == null) return;

        float offsetX = baseOffset;

        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("f-dash"))
        {
            offsetX = dashOffset;
        }

        Vector3 newPos = playerTransform.position + new Vector3(
            isFacingRight ? offsetX : -offsetX,
            0f,
            0f
        );

        transform.position = newPos;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Titik posisi wallCheck
        Gizmos.DrawWireSphere(transform.position, 0.2f);

        // Garis dari player ke wallCheck
        if (playerTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(playerTransform.position, transform.position);
        }
    }
}
