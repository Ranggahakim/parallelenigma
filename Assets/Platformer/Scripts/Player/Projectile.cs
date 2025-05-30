using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;

    private Vector2 startPos;
    private Vector2 moveDirection;
    private Transform target;
    private float maxRange;
    private bool hasTarget;

    public void Setup(Vector2 direction, Transform targetEnemy, float range)
    {
        startPos = transform.position;
        moveDirection = direction.normalized;
        target = targetEnemy;
        hasTarget = (target != null);
        maxRange = range;
    }

    void Update()
    {
        if (hasTarget && target != null)
        {
            float distanceToStart = Vector2.Distance(transform.position, startPos);
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if (distanceToStart > maxRange)
            {
                Destroy(gameObject);
                return;
            }

            Vector2 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(moveDirection * speed * Time.deltaTime);
            float traveled = Vector2.Distance(transform.position, startPos);
            if (traveled > maxRange)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Optional: kasih damage di sini
            Destroy(gameObject);
        }
    }
}
