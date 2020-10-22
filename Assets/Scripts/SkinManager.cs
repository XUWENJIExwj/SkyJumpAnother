using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkinManager : MonoBehaviour
{
    //[SerializeField] private GameObject skins = null;
    [SerializeField] private RectTransform[] skinIcon = null; 
    [SerializeField] private SkinSupport skinSupport = null;
    [SerializeField] private Color[] colors = null;
    [SerializeField] private Image player = null;
    [SerializeField] private Color playerColor = Color.white;
    [SerializeField] private int playerType = 0;
    [SerializeField] private Image[] images = null;
    [SerializeField] private AudioManager audioManager;

    public enum MoveState
    {
        MOVE_STATE_STOP_UP,
        MOVE_STATE_STOP_DOWN,
        MOVE_STATE_MOVING_UP,
        MOVE_STATE_MOVING_DOWN
    }

    [SerializeField] private MoveState moveState = MoveState.MOVE_STATE_STOP_DOWN;

    public void DisplaySkinStore()
    {
        if(moveState == MoveState.MOVE_STATE_STOP_DOWN)
        {
            moveState = MoveState.MOVE_STATE_MOVING_UP;
            for (int i = 0; i < skinIcon.Length; i++)
            {
                skinIcon[i].DOMoveY(skinIcon[i].position.y + (i + 1) * 160, 0.5f).OnComplete(() => { moveState = MoveState.MOVE_STATE_STOP_UP; });
            }
            audioManager.PlaySE(AudioManager.SE.SE_TITLE, 1, 0.5f);
        }
        else if (moveState == MoveState.MOVE_STATE_STOP_UP)
        {
            moveState = MoveState.MOVE_STATE_MOVING_DOWN;
            for (int i = 0; i < skinIcon.Length; i++)
            {
                skinIcon[i].DOMoveY(skinIcon[i].position.y - (i + 1) * 160, 0.5f).OnComplete(() => { moveState = MoveState.MOVE_STATE_STOP_DOWN; });
            }
            audioManager.PlaySE(AudioManager.SE.SE_TITLE, 1, 0.5f);
        }
    }

    public void SetColorA()
    {
        playerType = 0;
        playerColor = colors[0];
        player.color = colors[0];
        skinSupport.SetPlayerColorInfo(playerType, playerColor);
        audioManager.PlaySE(AudioManager.SE.SE_TITLE, 1, 0.5f);
    }

    public void SetColorB()
    {
        playerType = 1;
        playerColor = colors[1];
        player.color = colors[1];
        skinSupport.SetPlayerColorInfo(playerType, playerColor);
        audioManager.PlaySE(AudioManager.SE.SE_TITLE, 1, 0.5f);
    }
    public void SetColorC()
    {
        playerType = 2;
        playerColor = colors[2];
        player.color = colors[2];
        skinSupport.SetPlayerColorInfo(playerType, playerColor);
        audioManager.PlaySE(AudioManager.SE.SE_TITLE, 1, 0.5f);
    }
    public void SetColorD()
    {
        playerType = 3;
        playerColor = colors[3];
        player.color = colors[3];
        skinSupport.SetPlayerColorInfo(playerType, playerColor);
        audioManager.PlaySE(AudioManager.SE.SE_TITLE, 1, 0.5f);
    }

    public void InitInTitleScene()
    {
        playerColor = skinSupport.GetPlayerColor();
        playerType = skinSupport.GetPlayerType();
        player.color = playerColor;

        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = colors[i];
        }
    }

    public void SetSkinSupport(SkinSupport skin_support)
    {
        skinSupport = skin_support;
    }

    public void SetAudioManager(AudioManager audio_manager)
    {
        audioManager = audio_manager;
    }
}
