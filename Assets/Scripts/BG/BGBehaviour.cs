using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGBehaviour : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bgSpriteRenderer = null;
    [SerializeField] private Sprite[] bgSprites = null;
    [SerializeField] private int bgStep = 0;

    public enum BG_Sprites
    {
        BG_PLANE,
        BG_SKY_00,
        BG_SKY_01,
        BG_UNIVERSE
    }

    public void InitBg(int step_idx)
    {
        SetBgSprite((BG_Sprites)step_idx);
        SetBgSize();
        SetBgStep(step_idx);
        SetBgPosition();
    }

    public void SetBgSprite(BG_Sprites sprite_idx)
    {
        bgSpriteRenderer.sprite = bgSprites[(int)sprite_idx];
    }

    public void SetBgSize()
    {
        bgSpriteRenderer.size = ScreenInfo.bgSizeMatchX;
    }

    public void SetBgStep(int step_idx)
    {
        bgStep = step_idx;
    }

    public void SetBgPosition()
    {
        float pos_y = transform.position.y - ScreenInfo.bgPosYDeviation + bgStep * ScreenInfo.bgSizeMatchX.y;
        transform.position = new Vector3(transform.position.x, pos_y, transform.position.z);
    }
}
