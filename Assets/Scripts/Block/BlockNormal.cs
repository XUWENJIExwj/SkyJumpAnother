using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockNormal : MonoBehaviour
{
    public Rigidbody2D playerRb = null;
    public ObjectWithFlick player = null;
    public BoxCollider2D boxCollider;
    public Score score;
    public Trajectory trajectory;

    private bool hasScore = false;

    protected void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.GetPlayerState() == ObjectWithFlick.PlayerState.PLAYER_STATE_JUMP_DOWN)
        {
            player.SetIfOnBlock(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (player.GetPlayerState() == ObjectWithFlick.PlayerState.PLAYER_STATE_JUMP_DOWN ||
            (player.GetPlayerState() == ObjectWithFlick.PlayerState.PLAYER_STATE_JUMP_UP && player.GetPlayerOldState() == ObjectWithFlick.PlayerState.PLAYER_STATE_TAP)))
        {
            player.SetIfOnBlock(true);
            player.SetPlayerState(ObjectWithFlick.PlayerState.PLAYER_STATE_IDLE);
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
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
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = 0f;

            player.SetPlayerState(ObjectWithFlick.PlayerState.PLAYER_STATE_IDLE);

            SetScore();
        }
    }

    public void SetScore()
    {
        if (!hasScore)
        {
            hasScore = true;
            //score.SetScore(
            //    transform.position.y +
            //    Camera.main.orthographicSize + // Cameraによる座標調整
            //    boxCollider.size.y / 2 * transform.localScale.y // blockの中心から最高点の位置
            //    );
        }
    }
}
