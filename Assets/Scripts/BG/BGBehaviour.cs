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
    [SerializeField] private EnemyCreator enemyCreator = null;
    [SerializeField] private GameObject[] enemys = null;
    [SerializeField] private DecorationCreator decorationCreator = null;
    [SerializeField] private GameObject[] decorations = null;

    public enum BG_Sprites
    {
        BG_PLANE,
        BG_SKY_00,
        BG_SKY_01,
        BG_UNIVERSE
    }

    public enum Obj_Type
    {
        TYPE_BLOCK,
        TYPE_ENEMY,
        TYPE_NONE
    }

    public void InitBg(int step_idx)
    {
        SetBgSprite((BG_Sprites)step_idx);
        SetBgSize();
        SetBgStep(step_idx);
        SetBgPosition();

        CreateBlockAtGameStart(step_idx);
        CreateEnemyAtGameStart(step_idx);
        decorationCreator.SetStep(step_idx);
        decorations = decorationCreator.CreateObjs();
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

    public void CreateBlockAtGameStart(int step_idx)
    {
        if (step_idx != 0)
        {
            blocks = blockCreator.CreateObjs(bgPrevious.GetLastBlock());
        }
        else
        {
            blocks = blockCreator.CreateObjs();
            FixBlocksInStepZero();
        }
    }

    public void CreateEnemyAtGameStart(int step_idx)
    {
        enemyCreator.SetEnemyType(EnemyCreator.EnemyType.TYPE_BIRD);

        if (step_idx > 1)
        {
            enemys = enemyCreator.CreateObjs(bgPrevious.GetLastEnemy());
        }
        else if (step_idx == 1)
        {
            enemys = enemyCreator.CreateObjs();
        }
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
        if (transform.position.y + ScreenInfo.bgHalfSizeMatchX.y < cam.transform.position.y - ScreenInfo.screenHalfSize.y - ScreenInfo.bgPosYDeviationMatchX)
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
        DestroyObjs(enemys);
        DestroyObjs(decorations);

        blocks = blockCreator.CreateObjs(bgPrevious.GetLastBlock());
        enemyCreator.SetEnemyType(EnemyCreator.EnemyType.TYPE_UFO);
        enemys = enemyCreator.CreateObjs(bgPrevious.GetLastEnemy());
        decorationCreator.SetStep(bgStep);
        decorations = decorationCreator.CreateObjs();
    }

    public void DestroyObjs(GameObject[] objs)
    {
        if (objs != null)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                Destroy(objs[i]);
                objs[i] = null;
            }

            objs = null;
        }
    }

    public GameObject GetLastBlock()
    {
        if (blocks != null)
        {
            return blocks[blocks.Length - 1];
        }

        return null;
    }

    public GameObject GetLastEnemy()
    {
        if (enemys != null)
        {
            return enemys[enemys.Length - 1];
        }

        return null;
    }

    private void FixBlocksInStepZero()
    {
        blocks[0].SetActive(false);

        // 二個目のブロックの位置調整
        float blockX = Random.Range(-ScreenInfo.screenHalfSize.x, ScreenInfo.screenHalfSize.x);

        // 1.5個グリッド線からなるブロックの長さ
        while (Mathf.Abs(blockX) < blockCreator.GetObjSpaceRange().heightMin ||
            Mathf.Abs(blocks[2].transform.position.x - blockX) < blockCreator.GetObjSpaceRange().heightMin)
        {
            blockX = Random.Range(-ScreenInfo.screenHalfSize.x, ScreenInfo.screenHalfSize.x);
        }

        blocks[1].transform.position = new Vector3(blockX, blocks[1].transform.position.y, blocks[1].transform.position.z);
    }
}
