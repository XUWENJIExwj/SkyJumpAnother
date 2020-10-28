using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class ResultManager : CommonManager
{
    [SerializeField] private Score score = null;
    [SerializeField] private Vector2 scoreDeviation = Vector2.zero;
    [SerializeField] private Score scoreBest = null;
    [SerializeField] private Vector2 scoreBestDeviation = Vector2.zero;
    [SerializeField] private InputField inputer = null;
    [SerializeField] private CheckStringByte checkStringByte = null;
    [SerializeField] private ObjectProperty lightObj = null;
    [SerializeField] private Vector2 lightDeviation = Vector2.zero;
    [SerializeField] [Range(0.0f, 1.0f)] private float lightSizeSpeed = 0.0f;
    [SerializeField] private SpriteRenderer player = null;
    [SerializeField] private PlayerSoul playerSoul = null;
    [SerializeField] private bool hasCreateSoul = false; 

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

        FixPos(lightObj.gameObject, lightDeviation);

        SetPlayerSkinColor();

        playerSoul.gameObject.SetActive(false);
        playerSoul.SetSoulColor();
    }

    public override void UpdateThisScene()
    {
        SetLightSize();

        if (lightObj.transform.localScale.x >= lightObj.GetObjSize().x && !hasCreateSoul)
        {
            hasCreateSoul = true;
            playerSoul.gameObject.SetActive(true);
            playerSoul.PlayerSoulAnimation();
        }
    }

    private void LoadScoreInfo()
    {
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

    public void SetLightSize()
    {
        if (lightObj.transform.localScale.x < lightObj.GetObjSize().x && Time.fixedTime >= 0.3f)
        {
            lightObj.transform.localScale = new Vector3(lightObj.transform.localScale.x + lightSizeSpeed, lightObj.transform.localScale.y, lightObj.transform.localScale.z);
        }
    }

    public override void PrepareToGoToNextScene(string next_scene)
    {
        lightObj.gameObject.SetActive(false);
        InputNameCancel();
        base.PrepareToGoToNextScene(next_scene);
    }
}
