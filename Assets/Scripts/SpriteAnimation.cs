using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRender = null;
    [SerializeField] private Sprite[] sprites = null;
    [SerializeField] private float framesPerSec = 0.0f;

    public void PlayerIdle()
    {
        spriteRender.sprite = sprites[0];
    }

    public void PlayerTap()
    {
        spriteRender.sprite = sprites[1];
    }

    public void PlayerJump()
    {
        int index = (int)(Time.fixedTime * framesPerSec) % 2 + 2;
        spriteRender.sprite = sprites[index];
    }

    public void UpdateEnemyAnimation()
    {
        int index = (int)(Time.time * framesPerSec) % 2;
        spriteRender.sprite = sprites[index];
    }

    public void SetDirection(bool is_right)
    {
        if(is_right)
        {
            spriteRender.flipX = !is_right;
        }
        else
        {
            spriteRender.flipX = !is_right;
        }
    }
}
