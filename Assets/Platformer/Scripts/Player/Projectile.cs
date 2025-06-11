using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;

    private Vector2 startPos;
    private Vector2 moveDirection;
    private float maxRange;
    private bool isMoving = false; // Untuk menunda gerakan

    public void Setup(Vector2 direction, Transform targetEnemy, float range, float delay = 0.5f)
    {
        startPos = transform.position;
        moveDirection = direction.normalized;
        maxRange = range;

        // Flip sprite kalau ke kiri
        if (moveDirection.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        StartCoroutine(StartMovingAfterDelay(delay));
    }

    private IEnumerator StartMovingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isMoving = true;
    }

    void Update()
    {
        if (!isMoving) return;

        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);

        float traveled = Vector2.Distance(transform.position, startPos);
        if (traveled > maxRange)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Wall"))
        {
            StartCoroutine(DestroyAfterDelay(0.5f));
        }
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
