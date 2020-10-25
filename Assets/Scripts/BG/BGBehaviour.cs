using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGBehaviour : MonoBehaviour
{
    [SerializeField] private Camera cam = null;
    [SerializeField] private SpriteRenderer bgSpriteRenderer = null;
    [SerializeField] private Sprite[] bgSprites = null;
    [SerializeField] private int bgStep = 0;

    [SerializeField] private BGBehaviour bgPrevious = null;
    [SerializeField] private BlockCreator blockCreator = null;
    [SerializeField] private GameObject[] blocks = null;

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

        if (step_idx != 0)
        {
            blocks = blockCreator.CreateObjs(bgPrevious.GetLastBlock());
        }
        else
        {
            blocks = blockCreator.CreateObjs();
        }
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
        float pos_y = bgStep * ScreenInfo.bgSizeMatchX.y - ScreenInfo.bgPosYDeviationMatchX;
        transform.position = new Vector3(transform.position.x, pos_y, transform.position.z);
    }

    public void UpdateBg()
    {
        if(!CheckIfInCameraView())
        {
            RestBg();
        }
    }

    public bool CheckIfInCameraView()
    {
        if (transform.position.y + ScreenInfo.bgHalfSizeMatchX.y < cam.transform.position.y - ScreenInfo.screenHalfSize.y + ScreenInfo.bgPosYDeviationMatchX)
        {
            return false;
        }

        return true;
    }

    public void RestBg()
    {
        SetBgSprite(BG_Sprites.BG_UNIVERSE);
        bgStep += GameManager.bgNum;
        SetBgPosition();

        DestroyObjs(blocks);
        blocks = blockCreator.CreateObjs(bgPrevious.GetLastBlock());
    }

    public void DestroyObjs(GameObject[] objs)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            Destroy(objs[i]);
            objs[i] = null;
        }

        objs = null;
    }

    public GameObject GetLastBlock()
    {
        if (blocks == null)
        {
            return null;
        }

        return blocks[blocks.Length - 1];
    }
}
