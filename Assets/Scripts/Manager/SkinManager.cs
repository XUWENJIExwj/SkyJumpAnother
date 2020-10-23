using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkinManager : MonoBehaviour
{
    [SerializeField] private RectTransform[] skinIcon = null;
    [SerializeField] private Image[] images = null;
    [SerializeField] private Color[] colors = null;
    [SerializeField] private Image player = null;
    [SerializeField] private SkinInfo.SkinType playerType = SkinInfo.SkinType.SKIN_TYPE_00;
    [SerializeField] private Color playerColor = SkinInfo.skinColor;

    private enum MoveState
    {
        MOVE_STATE_STOP_UP,
        MOVE_STATE_STOP_DOWN,
        MOVE_STATE_MOVING_UP,
        MOVE_STATE_MOVING_DOWN
    }

    [SerializeField] private MoveState moveState = MoveState.MOVE_STATE_STOP_DOWN;
    [SerializeField] [Range(0.0f, 1.0f)] private float iconSpaceCoefficient = 0.0f;
    [SerializeField] [Range(0.0f, 1.0f)] private float moveTime = 0.0f;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        playerType = SkinInfo.skinType;
        playerColor = SkinInfo.skinColor;
        player.color = SkinInfo.skinColor;

        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = colors[i];
        }
    }

    public void SetColorA()
    {
        playerType = SkinInfo.SkinType.SKIN_TYPE_00;
        playerColor = colors[0];
        player.color = colors[0];

        SkinInfo.SetSkinInfo(SkinInfo.SkinType.SKIN_TYPE_00, colors[0]);
        AudioManager.PlaySE(AudioManager.AudioSourceIndex.AUDIO_SOURCE_SE_TYPE_A, AudioManager.SE.SE_TITLE, 0.5f);
    }

    public void SetColorB()
    {
        playerType = SkinInfo.SkinType.SKIN_TYPE_01;
        playerColor = colors[1];
        player.color = colors[1];

        SkinInfo.SetSkinInfo(SkinInfo.SkinType.SKIN_TYPE_01, colors[1]);
        AudioManager.PlaySE(AudioManager.AudioSourceIndex.AUDIO_SOURCE_SE_TYPE_A, AudioManager.SE.SE_TITLE, 0.5f);
    }
    public void SetColorC()
    {
        playerType = SkinInfo.SkinType.SKIN_TYPE_02;
        playerColor = colors[2];
        player.color = colors[2];

        SkinInfo.SetSkinInfo(SkinInfo.SkinType.SKIN_TYPE_02, colors[2]);
        AudioManager.PlaySE(AudioManager.AudioSourceIndex.AUDIO_SOURCE_SE_TYPE_A, AudioManager.SE.SE_TITLE, 0.5f);
    }
    public void SetColorD()
    {
        playerType = SkinInfo.SkinType.SKIN_TYPE_03;
        playerColor = colors[3];
        player.color = colors[3];

        SkinInfo.SetSkinInfo(SkinInfo.SkinType.SKIN_TYPE_03, colors[3]);
        AudioManager.PlaySE(AudioManager.AudioSourceIndex.AUDIO_SOURCE_SE_TYPE_A, AudioManager.SE.SE_TITLE, 0.5f);
    }

    public void DisplaySkinStore()
    {
        if (moveState == MoveState.MOVE_STATE_STOP_DOWN)
        {
            moveState = MoveState.MOVE_STATE_MOVING_UP;

            float icon_space = ScreenInfo.screenSize.y * iconSpaceCoefficient;

            for (int i = 1; i < skinIcon.Length; i++)
            {
                float pos_y = skinIcon[0].position.y + i * icon_space;
                skinIcon[i].DOMoveY(pos_y, moveTime).OnComplete(() => { moveState = MoveState.MOVE_STATE_STOP_UP; });
            }

            AudioManager.PlaySE(AudioManager.AudioSourceIndex.AUDIO_SOURCE_SE_TYPE_A, AudioManager.SE.SE_TITLE, 0.5f);
        }
        else if (moveState == MoveState.MOVE_STATE_STOP_UP)
        {
            moveState = MoveState.MOVE_STATE_MOVING_DOWN;

            for (int i = 0; i < skinIcon.Length; i++)
            {
                skinIcon[i].DOMoveY(skinIcon[0].position.y, moveTime).OnComplete(() => { moveState = MoveState.MOVE_STATE_STOP_DOWN; });
            }

            AudioManager.PlaySE(AudioManager.AudioSourceIndex.AUDIO_SOURCE_SE_TYPE_A, AudioManager.SE.SE_TITLE, 0.5f);
        }
    }
}
