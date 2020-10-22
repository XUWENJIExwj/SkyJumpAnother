using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockIce : BlockNormal
{
    public PhysicsMaterial2D ice;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (playerObjWithFlick.playerState == ObjectWithFlick.PlayerState.PLAYER_STATE_JUMP_DOWN ||
            (playerObjWithFlick.playerState == ObjectWithFlick.PlayerState.PLAYER_STATE_JUMP_UP && playerObjWithFlick.oldPlayerState == ObjectWithFlick.PlayerState.PLAYER_STATE_TAP)))
        {
            playerObjWithFlick.SetIfOnBlock(true);
            playerObjWithFlick.SetPlayerState(ObjectWithFlick.PlayerState.PLAYER_STATE_IDLE);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerRb.sharedMaterial = null;

            if (playerObjWithFlick.playerState == ObjectWithFlick.PlayerState.PLAYER_STATE_IDLE ||
                playerObjWithFlick.playerState == ObjectWithFlick.PlayerState.PLAYER_STATE_TAP)
            {
                playerObjWithFlick.SetPlayerState(ObjectWithFlick.PlayerState.PLAYER_STATE_JUMP_DOWN);
                GetTrajectory().Hide();
            }

            playerObjWithFlick.SetIfOnBlock(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerObjWithFlick.GetIfOnBlock())
        {
            playerRb.sharedMaterial = ice;

            playerObjWithFlick.SetPlayerState(ObjectWithFlick.PlayerState.PLAYER_STATE_IDLE);

            SetScore();
        }
    }
}
