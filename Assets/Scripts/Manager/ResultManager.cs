using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using DG.Tweening;

public class ResultManager : CommonManager
{
    [SerializeField] private Score score = null;
    [SerializeField] private Vector2 scoreDeviation = Vector2.zero;
    [SerializeField] private Score scoreBest = null;
    [SerializeField] private Vector2 scoreBestDeviation = Vector2.zero;
    [SerializeField] private InputField inputer = null;
    [SerializeField] private CheckStringByte checkStringByte = null;
    [SerializeField] private ObjectProperty ufo = null;
    [SerializeField] private Vector3 ufoDeviation = Vector3.zero;
    [SerializeField] private ObjectProperty lightObj = null;
    [SerializeField] [Range(0.0f, 5.0f)] private float lightTime = 0.0f;
    [SerializeField] [Range(0.0f, 5.0f)] private float lightDelayTime = 0.0f;
    [SerializeField] private SpriteRenderer player = null;
    [SerializeField] private Vector3 playerDeviation = Vector3.zero;
    [SerializeField] private PlayerSoul playerSoul = null;
    [SerializeField] private Vector3 playerSoulDeviation = Vector3.zero;

    // Debug用
    //private Vector3 posFix;

    protected override void Start()
    {
        SetBgSize();
        AudioManager.StopBGM();
        AudioManager.PlayBGM(AudioManager.BGM.BGM_RESULT);
        base.Start();

        inputer.gameObject.SetActive(false);

        CheckRankInfo();
        LoadScoreInfo();

        FixPos(score.gameObject, scoreDeviation);
        FixPos(scoreBest.gameObject, scoreBestDeviation);

        InitLight();
        SetLightOn();

        // Debug用
        //posFix = playerSoul.transform.position;

        FixPos(ufo.gameObject, ufoDeviation);

        SetPlayerSkinColor();
        FixPos(player.gameObject, playerDeviation);

        playerSoul.gameObject.SetActive(false);
        playerSoul.SetSoulColor();
        FixPos(playerSoul.gameObject, playerSoulDeviation);
    }

    public override void UpdateThisScene()
    {
        // Debug用
        //FixPos(playerSoul.gameObject, posFix, playerSoulDeviation);
    }

    private void LoadScoreInfo()
    {
        //score.SetScore(0);
        //scoreBest.SetScore(0);
        score.SetScore(RankInfo.GetNewRankInfo().score);
        scoreBest.SetScore(RankInfo.GetRankInfo(0).score);
    }

    private void CheckRankInfo()
    {
        RankInfo.LoadRank();

        if (RankInfo.CheckIfRankIn())
        {
            inputer.gameObject.SetActive(true);

            RankInfo.SortRank();
        }
    }

    private void FixPos(GameObject game_object, Vector3 deviation)
    {
        game_object.transform.position = new Vector3(
            game_object.transform.position.x + (ScreenInfo.screenCoefficient.y / ScreenInfo.screenCoefficient.x - 1) * deviation.x,
            game_object.transform.position.y + (ScreenInfo.screenCoefficient.y / ScreenInfo.screenCoefficient.x - 1) * deviation.y,
            game_object.transform.position.z);
    }

    // Debug用
    //private void FixPos(GameObject game_object, Vector3 pos_fix, Vector3 deviation)
    //{
    //    game_object.transform.position = new Vector3(
    //        posFix.x + (ScreenInfo.screenCoefficient.y / ScreenInfo.screenCoefficient.x - 1) * deviation.x,
    //        posFix.y + (ScreenInfo.screenCoefficient.y / ScreenInfo.screenCoefficient.x - 1) * deviation.y,
    //        posFix.z);
    //}

    public void SetPlayerSkinColor()
    {
        player.color = SkinInfo.skinLitColor;
    }

    public void InputNameOK()
    {
        int idx = RankInfo.GetNewRankInfo().rank - 1;

        bool all_blank = true;

        for (int i = 0; i < inputer.textComponent.text.Length; i++)
        {
            if (inputer.textComponent.text[i] != 0x20)
            {
                all_blank = false;
                break;
            }
        }

        if (!all_blank)
        {
            RankInfo.InputRankName(inputer.textComponent.text, idx);
        }

        checkStringByte.MakeCharacterToLimit(RankInfo.GetRankInfo(idx).name);
        RankInfo.SaveRank(idx);

        AudioManager.PlaySE(AudioManager.AudioSourceIndex.AUDIO_SOURCE_SE_TYPE_A, AudioManager.SE.SE_TITLE, 0.5f);
        inputer.gameObject.SetActive(false);
    }

    public void InputNameCancel()
    {
        inputer.textComponent.text = "";

        AudioManager.PlaySE(AudioManager.AudioSourceIndex.AUDIO_SOURCE_SE_TYPE_A, AudioManager.SE.SE_TITLE, 0.5f);
        inputer.gameObject.SetActive(false);
    }

    public void InitLight()
    {
        lightObj.SetSize();
        lightObj.SetPos();
        lightObj.transform.localScale = new Vector3(0.0f, lightObj.transform.localScale.y, lightObj.transform.localScale.z);
    }

    public void SetLightOn()
    {
        lightObj.transform.DOScaleX(lightObj.GetObjSize().x, lightTime).SetDelay(lightDelayTime).OnComplete(() => CreateSoul());
    }

    public void SetLightOff()
    {
        lightObj.transform.DOScaleX(0, lightTime);
    }

    public override void GoToNextScene()
    {
        base.GoToNextScene();
    }

    public override void PrepareToGoToNextScene(string next_scene)
    {
        if (fade.GetFadeState() == Fade.FadeState.FADE_STATE_NONE)
        {
            SetLightOff();
            InputNameCancel();
        }
        
        base.PrepareToGoToNextScene(next_scene);
    }

    public void CreateSoul()
    {
        playerSoul.gameObject.SetActive(true);
        playerSoul.PlayerSoulAnimation();
    }
}
