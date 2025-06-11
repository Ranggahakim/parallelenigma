using UnityEngine;
using System.Collections;

public class DashController
{
    private PlayerMovement player;

    private bool canDash = true;

    public DashController(PlayerMovement player)
    {
        this.player = player;
    }

    public void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.canMove && canDash)
        {
            player.StartCoroutine(Dash());
        }
    }

    public void HandleDownDash()
    {
        if (Input.GetKeyDown(KeyCode.S) && !player.isWallSliding && !player.onGround && IsHighEnoughToDownDash())
        {
            player.isDownDashing = true;
            player.rb.linearVelocity = new Vector2(0f, -player.downDashPower);
            player.animator.SetBool("isDownDash", true);
            player.canMove = false;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        player.isDashing = true;
        player.animator.SetBool("isDashing", true);
        float originalGravity = player.rb.gravityScale;
        player.rb.gravityScale = 0f;
        player.rb.linearVelocity = new Vector2(player.transform.localScale.x * player.dashPower, 0f);

        yield return new WaitForSeconds(player.dashTime);

        player.rb.gravityScale = originalGravity;
        player.isDashing = false;
        player.animator.SetBool("isDashing", false);

        yield return new WaitForSeconds(player.dashCooldown);
        canDash = true;
    }

    private bool IsHighEnoughToDownDash()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.down, player.minDownDashHeight, player.groundLayer);
        return !hit.collider;
    }
}
