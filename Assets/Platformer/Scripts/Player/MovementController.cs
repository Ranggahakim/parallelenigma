using UnityEngine;
using System.Collections;

public class MovementController
{
    private PlayerMovement player;
    private DashController dashController;

    public MovementController(PlayerMovement player)
    {
        this.player = player;
    }

    public MovementController(DashController dashController)
    {
        this.dashController = dashController;
    }

    public void Move()
    {
        if (!player.canMove || player.isLanding) return;

        if (player.isWallJumping)
            return;

        if (!IsHittingWall())
        {
            player.rb.linearVelocity = new Vector2(player.horizontalInput * player.moveSpeed, player.rb.linearVelocity.y);
        }
        else
        {
            player.rb.linearVelocity = new Vector2(0f, player.rb.linearVelocity.y);
        }

        player.animator.SetFloat("xVelocity", Mathf.Abs(player.rb.linearVelocity.x));
        player.animator.SetFloat("yVelocity", player.rb.linearVelocity.y);
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(player.groundCheck.position, 0.2f, player.groundLayer);
    }

    public bool IsHittingWall()
    {
        return Physics2D.OverlapCircle(player.wallCheck.position, 0.2f, player.wallLayer);
    }

    public void FlipSprite()
    {
        if (player.isFacingRight && player.horizontalInput < 0f ||
            !player.isFacingRight && player.horizontalInput > 0f)
        {
            player.isFacingRight = !player.isFacingRight;
            Vector3 localScale = player.transform.localScale;
            localScale.x *= -1f;
            player.transform.localScale = localScale;
        }
    }

    public IEnumerator ResetLanding()
    {
        yield return new WaitForSeconds(0.2f);
        player.isLanding = false;
        player.animator.SetBool("isLanding", false);
    }
}
