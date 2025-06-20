using System.Collections;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float spawnDistance = 1f;
    public float maxRange = 4f;
    public float attackCooldown = 2f;

    private float cooldownTimer = 0f;

    private PlayerMovement player;
    private Rigidbody2D rb;
    private MovementController movementController;

    void Start()
    {
        player = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Hitung mundur cooldown
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && player.onGround && cooldownTimer <= 0f)
        {
            FireProjectile();
            if (player != null && player.animator != null)
            {
                player.animator.SetBool("isAttack", true);
                StartCoroutine(ResetAttackAnim());
                player.canMove = false;
                if (player.rb != null)
                {
                    player.rb.linearVelocity = Vector2.zero;
                }
            }

            cooldownTimer = attackCooldown; // ⏲ mulai cooldown
        }
    }

    public void FireProjectile()
    {
        // Cek arah karakter (kanan atau kiri)
        float facingDir = transform.localScale.x > 0 ? 1f : -1f;
        Vector2 direction = new Vector2(facingDir, 0); // Hanya horizontal

        Vector2 spawnPos = (Vector2)transform.position + direction * spawnDistance;
        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        projectile.transform.position = new Vector3(projectile.transform.position.x, projectile.transform.position.y, 0); // jaga z = 0

        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.Setup(direction, null, maxRange); // tanpa target
        }
    }

    IEnumerator ResetAttackAnim()
    {
        yield return new WaitForSeconds(1f);
        player.animator.SetBool("isAttack", false);
        player.canMove = true;
    }
}
