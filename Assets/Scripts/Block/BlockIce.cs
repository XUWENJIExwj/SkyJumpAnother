using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockIce : BlockNormal
{
    public PhysicsMaterial2D ice;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (player.GetPlayerState() == ObjectWithFlick.PlayerState.PLAYER_STATE_JUMP_DOWN ||
            (player.GetPlayerState() == ObjectWithFlick.PlayerState.PLAYER_STATE_JUMP_UP && player.GetPlayerOldState() == ObjectWithFlick.PlayerState.PLAYER_STATE_TAP)))
        {
            player.SetIfOnBlock(true);
            player.SetPlayerState(ObjectWithFlick.PlayerState.PLAYER_STATE_IDLE);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerRb.sharedMaterial = null;

            if (player.GetPlayerState() == ObjectWithFlick.PlayerState.PLAYER_STATE_IDLE ||
                player.GetPlayerState() == ObjectWithFlick.PlayerState.PLAYER_STATE_TAP)
            {
                player.SetPlayerState(ObjectWithFlick.PlayerState.PLAYER_STATE_JUMP_DOWN);
                trajectory.Hide();
            }

            player.SetIfOnBlock(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.GetIfOnBlock())
        {
            playerRb.sharedMaterial = ice;

            player.SetPlayerState(ObjectWithFlick.PlayerState.PLAYER_STATE_IDLE);

            SetScore();
        }
    }
}
