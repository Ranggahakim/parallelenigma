using System.Collections;
using UnityEngine;

public class WallController
{
    private PlayerMovement player;
    private float wallJumpingCounter;
    private float wallJumpingDirection;
    private bool isWallSliding;

    public WallController(PlayerMovement player)
    {
        this.player = player;
    }

    public void HandleWallSlide()
    {
        if (IsWalled() && !player.onGround)
        {
            isWallSliding = true;
            player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, Mathf.Clamp(player.rb.linearVelocity.y, -player.wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }

        player.animator.SetBool("isWallSliding", isWallSliding);

        if (isWallSliding)
        {
            wallJumpingDirection = -player.transform.localScale.x;
            wallJumpingCounter = player.wallJumpingTime;
            player.CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
    }

    public void HandleWallJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0f)
        {
            player.isWallJumping = true;
            player.rb.linearVelocity = new Vector2(wallJumpingDirection * player.wallJumpingPower.x, player.wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (player.transform.localScale.x != wallJumpingDirection)
            {
                player.isFacingRight = !player.isFacingRight;
                Vector3 localScale = player.transform.localScale;
                localScale.x *= -1f;
                player.transform.localScale = localScale;
            }

            player.StartCoroutine(StopWallJumpingAfterTime(player.wallJumpingDuration));
        }
    }

    private void StopWallJumping()
    {
        player.isWallJumping = false;
    }

    private IEnumerator StopWallJumpingAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        player.isWallJumping = false;
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(player.wallCheck.position, 0.2f, player.wallLayer);
    }
}
