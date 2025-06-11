using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    public Animator animator;
    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    [Header("Settings")]
    public float moveSpeed = 7f;
    public float jumpPower = 7f;
    public float downDashPower = 20f;
    public float minDownDashHeight = 2f;
    public float dashPower = 24f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;
    public float wallSlidingSpeed = 2f;
    public float wallJumpingTime = 0.2f;
    public float wallJumpingDuration = 0.6f;
    public Vector2 wallJumpingPower = new Vector2(7f, 7f);

    [HideInInspector] public float horizontalInput;
    [HideInInspector] public bool onGround = true;
    [HideInInspector] public bool isFacingRight = true;
    [HideInInspector] public bool isDashing = false;
    [HideInInspector] public bool isWallSliding = false;
    [HideInInspector] public bool isWallJumping = false;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool isLanding = false;
    [HideInInspector] public bool isDownDashing = false;

    private MovementController movementController;
    private JumpController jumpController;
    private DashController dashController;
    private WallController wallController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        movementController = new MovementController(this);
        jumpController = new JumpController(this);
        dashController = new DashController(this);
        wallController = new WallController(this);
    }

    private void Update()
    {
        if (isDashing || isDownDashing) return;

        horizontalInput = Input.GetAxisRaw("Horizontal");
        onGround = movementController.IsGrounded();
        animator.SetBool("isJumping", !onGround);

        jumpController.HandleJump();
        dashController.HandleDownDash();
        dashController.HandleDash();

        wallController.HandleWallSlide();
        wallController.HandleWallJump();

        if (!isWallJumping)
            movementController.FlipSprite();
    }

    private void FixedUpdate()
    {
        if (!canMove || isDashing || isDownDashing) return;

        movementController.Move();
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
            StartCoroutine(movementController.ResetLanding());

            canMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onGround = true;
        animator.SetBool("isJumping", false);
    }
}
