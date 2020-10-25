using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWithFlick : MonoBehaviour
{
    [SerializeField] private Camera cam = null;
    [SerializeField] private ObjectProperty objectProperty = null;
    [SerializeField] private Rigidbody2D playerRb = null;
    [SerializeField] [Range(0.0f, 1.0f)] private float startPosYCoefficient = 0.0f;

    [HideInInspector] public Vector3 pos { get { return transform.position; } }

    public enum PlayerState
    {
        PLAYER_STATE_IDLE,
        PLAYER_STATE_TAP,
        PLAYER_STATE_JUMP_UP,
        PLAYER_STATE_JUMP_DOWN
    }

    [SerializeField] private PlayerState playerOldState = PlayerState.PLAYER_STATE_IDLE;
    [SerializeField] private PlayerState playerState = PlayerState.PLAYER_STATE_IDLE;
    [SerializeField] private SpriteAnimation playerAnimation = null;

    [SerializeField] private float oldPosY = 0.0f;
    [SerializeField] private bool isRight = true;
    [SerializeField] private bool isOnBlock = false;
    [SerializeField] private bool isGameOver = false;

    private void Awake()
    {   
        playerRb.freezeRotation = true;
        transform.position = new Vector3(
            transform.position.x,
            -ScreenInfo.bgHalfSizeMatchX.y * startPosYCoefficient - ScreenInfo.bgPosYDeviationMatchX,
            transform.position.z);
    }

    public void UpdatePlayer()
    {
        switch (playerState)
        {
            case PlayerState.PLAYER_STATE_IDLE:
                UpdateStateIdle();
                break;
            case PlayerState.PLAYER_STATE_TAP:
                UpdateStateTap();
                break;
            case PlayerState.PLAYER_STATE_JUMP_UP:
                UpdateStateJumpUp();
                break;
            case PlayerState.PLAYER_STATE_JUMP_DOWN:
                UpdateStateJumpDown();
                break;
            default:
                break;
        }

        // 画面外チェック
        if (objectProperty.GetObjBorder().right < -ScreenInfo.screenHalfSize.x ||
            objectProperty.GetObjBorder().left > ScreenInfo.screenHalfSize.x ||
            transform.position.y < cam.transform.position.y - ScreenInfo.screenHalfSize.y)
        {
            SetGameOver();
        }
    }

    public void SetJump(Vector2 force)
    {
        playerRb.AddForce(force, ForceMode2D.Impulse);
        AudioManager.PlaySE(AudioManager.AudioSourceIndex.AUDIO_SOURCE_SE_TYPE_A, AudioManager.SE.SE_GAME_PLAYER_JUMP, 1);
    }

    public void ActivateRb()
    {
        playerRb.isKinematic = false;
    }

    public void DisactivateRb(bool unlimited_jump)
    {
        if(unlimited_jump)
        {
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = 0f;
            playerRb.isKinematic = true;
        }
        else
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0.0f);
        }
    }

    public void SetPlayerState(PlayerState player_state)
    {
        playerOldState = playerState;
        playerState = player_state;
    }

    public void UpdateDirection(float direction)
    {
        if (direction >= 0)
        {
            isRight = true;
        }
        else
        {
            isRight = false;
        }

        playerAnimation.SetDirection(isRight);
    }

    private void UpdateStateIdle()
    {
        playerAnimation.PlayerIdle();
    }

    private void UpdateStateTap()
    {
        playerAnimation.PlayerTap();

        oldPosY = transform.position.y;
    }

    private void UpdateStateJumpUp()
    {
        playerAnimation.PlayerJump();

        if (oldPosY >= transform.position.y)
        {
            SetPlayerState(PlayerState.PLAYER_STATE_JUMP_DOWN);
        }
        else
        {
            oldPosY = transform.position.y;
        }
    }

    private void UpdateStateJumpDown()
    {
        playerAnimation.PlayerJump();
    }

    public PlayerState GetPlayerState()
    {
        return playerState;
    }

    public PlayerState GetPlayerOldState()
    {
        return playerOldState;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plane") && playerState == PlayerState.PLAYER_STATE_JUMP_DOWN)
        {
            SetIfOnBlock(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plane") && (playerState == PlayerState.PLAYER_STATE_JUMP_DOWN ||
            (playerState == PlayerState.PLAYER_STATE_JUMP_UP && playerOldState == PlayerState.PLAYER_STATE_TAP)))
        {
            SetIfOnBlock(true);
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = 0f;
            SetPlayerState(PlayerState.PLAYER_STATE_IDLE);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            SetIfOnBlock(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plane") && GetIfOnBlock())
        {
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = 0f;

            SetPlayerState(PlayerState.PLAYER_STATE_IDLE);
        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(!AudioManager.GetIsPlaying(AudioManager.AudioSourceIndex.AUDIO_SOURCE_SE_TYPE_B))
            {
                AudioManager.PlaySE(AudioManager.AudioSourceIndex.AUDIO_SOURCE_SE_TYPE_B, AudioManager.SE.SE_GAME_PLAYER_COLLISION);
            }
        }
    }

    public void SetIfOnBlock(bool is_on_block)
    {
        isOnBlock = is_on_block;
    }

    public bool GetIfOnBlock()
    {
        return isOnBlock;
    }

    public bool GetIfGameOver()
    {
        return isGameOver;
    }

    public void SetGameOver()
    {
        // GameOver処理
        isGameOver = true;
    }
}
