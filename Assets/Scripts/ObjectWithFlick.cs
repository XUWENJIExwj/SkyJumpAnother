using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWithFlick : MonoBehaviour
{
    public float screenWidth;
    public float halfScreenWidth;
    public float screenHeight;
    public float halfScreenHeight;
    public float halfSize;
    public float posLeft;
    public float posRight;

    [HideInInspector] public Rigidbody2D playerRb;
    [HideInInspector] public Collider2D playerCollider;

    public AudioManager audioManager;

    private float oldPosY;

    [HideInInspector] public Vector3 pos { get { return transform.position; } }

    public enum PlayerState
    {
        PLAYER_STATE_IDLE,
        PLAYER_STATE_TAP,
        PLAYER_STATE_JUMP_UP,
        PLAYER_STATE_JUMP_DOWN
    }

    public PlayerState oldPlayerState;
    public PlayerState playerState;
    private SpriteAnimation playerAnimation;

    public bool isRight;

    public bool isOnBlock;

    private bool isGameOver;

    void Awake()
    {
        screenWidth = (float)Screen.width / 100;
        halfScreenWidth = screenWidth / 2;
        screenHeight = (float)Screen.height / 100;
        halfScreenHeight = screenHeight / 2;

        RectTransform rect = GetComponent<RectTransform>();
        halfSize = rect.sizeDelta.x * rect.localScale.x / 2;

        playerRb = GetComponent<Rigidbody2D>();
        //col = GetComponent<Collider2D>();

        playerCollider = null;
        Collider2D[] cols = GetComponents<Collider2D>();
        for (int i = 0; i < cols.Length; i++)
        {
            if(cols[i].isActiveAndEnabled)
            {
                playerCollider = cols[i];
            }
        }

        if(playerCollider)
        {
            //Debug.Log(playerCollider.GetType());
        }
        
        playerRb.freezeRotation = true;

        oldPlayerState = playerState = PlayerState.PLAYER_STATE_IDLE;

        playerAnimation = GetComponent<SpriteAnimation>();

        isRight = true;

        SetIfOnBlock(false);

        isGameOver = false;
    }

    private void FixedUpdate()
    {
        switch (playerState)
        {
            case PlayerState.PLAYER_STATE_IDLE:
                Update_Idle();
                break;
            case PlayerState.PLAYER_STATE_TAP:
                Update_Tap();
                break;
            case PlayerState.PLAYER_STATE_JUMP_UP:
                Update_Jump_Up();
                break;
            case PlayerState.PLAYER_STATE_JUMP_DOWN:
                Update_Jump_Down();
                break;
            default:
                break;
        }

        posLeft = transform.position.x + halfSize;
        posRight = transform.position.x - halfSize;

        // 画面外チェック
        if (posLeft < -halfScreenWidth ||
            posRight > halfScreenWidth ||
            transform.position.y < Camera.main.transform.position.y - halfScreenHeight)
        {
            SetGameOver();
        }
    }

    public void Push(Vector2 force)
    {
        playerRb.AddForce(force, ForceMode2D.Impulse);
        audioManager.PlaySE(AudioManager.SE.SE_GAME_PLAYER_JUMP, 1);
    }

    public void ActivateRb()
    {
        playerRb.isKinematic = false;
    }

    public void DisactivateRb(bool continue_jump)
    {
        if(continue_jump)
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
        oldPlayerState = playerState;
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

    private void Update_Idle()
    {
        playerAnimation.PlayerIdle();
    }

    private void Update_Tap()
    {
        playerAnimation.PlayerTap();

        oldPosY = transform.position.y;
    }

    private void Update_Jump_Up()
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

    private void Update_Jump_Down()
    {
        playerAnimation.PlayerJump();
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
            (playerState == PlayerState.PLAYER_STATE_JUMP_UP && oldPlayerState == PlayerState.PLAYER_STATE_TAP)))
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
            if(!audioManager.GetIsPlaying(2))
            {
                audioManager.PlaySE(AudioManager.SE.SE_GAME_PLAYER_COLLISION, 2);
            }

            //SetGameOver();
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
