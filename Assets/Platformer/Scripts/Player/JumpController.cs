using UnityEngine;

public class JumpController
{
    private PlayerMovement player;

    public JumpController(PlayerMovement player)
    {
        this.player = player;
    }

    public void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && player.onGround)
        {
            player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, player.jumpPower);
        }
    }
}
