using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockNormal : MonoBehaviour
{
    public Rigidbody2D playerRb = null;
    public ObjectWithFlick playerObjWithFlick = null;
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
        if (collision.gameObject.CompareTag("Player") && playerObjWithFlick.playerState == ObjectWithFlick.PlayerState.PLAYER_STATE_JUMP_DOWN)
        {
            playerObjWithFlick.SetIfOnBlock(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (playerObjWithFlick.playerState == ObjectWithFlick.PlayerState.PLAYER_STATE_JUMP_DOWN ||
            (playerObjWithFlick.playerState == ObjectWithFlick.PlayerState.PLAYER_STATE_JUMP_UP && playerObjWithFlick.oldPlayerState == ObjectWithFlick.PlayerState.PLAYER_STATE_TAP)))
        {
            playerObjWithFlick.SetIfOnBlock(true);
            playerObjWithFlick.SetPlayerState(ObjectWithFlick.PlayerState.PLAYER_STATE_IDLE);
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
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
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = 0f;

            playerObjWithFlick.SetPlayerState(ObjectWithFlick.PlayerState.PLAYER_STATE_IDLE);

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

    public Trajectory GetTrajectory()
    {
        return trajectory;
    }
}
