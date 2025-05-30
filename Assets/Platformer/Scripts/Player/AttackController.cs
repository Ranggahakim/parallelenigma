using UnityEngine;

public class AttackController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float spawnDistance = 2f;
    public float maxRange = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        GameObject targetEnemy = FindClosestEnemyInRange(maxRange);
        Vector2 direction;

        if (targetEnemy != null)
        {
            direction = (targetEnemy.transform.position - transform.position).normalized;
        }
        else
        {
            // Kalau gak ada musuh, tembak lurus ke arah mouse
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mouseWorld - transform.position).normalized;
        }

        Vector2 spawnPos = (Vector2)transform.position + direction * spawnDistance;
        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        Projectile projScript = projectile.GetComponent<Projectile>();

        if (projScript != null)
        {
            projScript.Setup(direction, targetEnemy != null ? targetEnemy.transform : null, maxRange);

        }
    }

    GameObject FindClosestEnemyInRange(float range)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float minDist = range;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = enemy;
            }
        }
        return closest;
    }
}
