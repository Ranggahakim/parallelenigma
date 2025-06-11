using System;
using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private float moveSpeed = 7f;
    private float horizontalInput;
    public float jumpPower = 7f;
    public bool onGround = true;
    private bool isFacingRight = true;

    public float downDashPower = 20f;
    private bool isDownDashing = false;
    private bool isLanding;
    private bool canMove = true;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.5f;
    private Vector2 wallJumpingPower = new Vector2(7f, 7f);

    private Animator animator;

    private bool canDash = true;
    private bool isDashing;
    float dashingPower = 24f;
    float dashingTime = 0.2f;
    float dashingCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing) return;

        if (isDownDashing) return;

        horizontalInput = Input.GetAxisRaw("Horizontal");

        onGround = IsGrounded();
        animator.SetBool("isJumping", !onGround);

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }

        if (Input.GetKeyDown(KeyCode.S) && !onGround)
        {
            isDownDashing = true;
            rb.linearVelocity = new Vector2(0f, -downDashPower);
            animator.SetBool("isDownDash", true);
            SetCanMove(false);
        }

        WallSlide();
        WallJump();
        if (!isWallJumping)
        {
            flipSprite();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing) return;

        if (isDownDashing) return;

        if (!isWallJumping)
        {
            if (canMove && !isLanding)
            {
                if (!IsHittingWall())
                {
                    rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
                }
                else
                {
                    rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
                }
            }
            animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocity.x));
            animator.SetFloat("yVelocity", rb.linearVelocity.y);
        }
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (isWalled() && !onGround)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSlidingSpeed, float.MaxValue));

        }
        else
        {
            isWallSliding = false;
        }

        animator.SetBool("isWallSliding", isWallSliding);
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    void flipSprite()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator ResetLanding()
    {
        yield return new WaitForSeconds(0.2f);
        isLanding = false;
        animator.SetBool("isLanding", false);
    }


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void SetCanMove(bool move)
    {
        canMove = move;
    }
    private bool IsHittingWall()
    {
        if (horizontalInput > 0)
        {
            return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
        }
        else if (horizontalInput < 0)
        {
            return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onGround = true;
        animator.SetBool("isJumping", !onGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onGround = true;

        if (isDownDashing)
        {
            isDownDashing = false;
            animator.SetBool("isDownDash", false);

            isLanding = true;
            animator.SetBool("isLanding", true);

            StartCoroutine(ResetLanding());

            SetCanMove(true);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        animator.SetBool("isDashing", true);

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);

        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("isDashing", false);

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(wallCheck.position, 0.2f);
    }
}