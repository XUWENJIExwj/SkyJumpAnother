using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankManger : CommonManager
{
    //public GameObject canvas;
    [SerializeField] private Text[] rankText = null;
    [SerializeField] private Text[] nameText = null;
    [SerializeField] private Text[] scoreText = null;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        RankInfo.LoadRank();
        //rank.LoadRankBinary();

        for (int i = 0; i < 5; i++)
        {
            rankText[i].text = (i + 1).ToString();

            if (RankInfo.GetRankInfo(i).rank != 0)
            {
                nameText[i].text = RankInfo.GetRankInfo(i).name;
                scoreText[i].text = RankInfo.GetRankInfo(i).score.ToString();
            }
            else
            {
                nameText[i].text = "---------";
                scoreText[i].text = "-----";
            }
        }

        AudioManager.PlayBGM(AudioManager.BGM.BGM_TITLE);
        base.Start();
        //fade.SetFadeState(Fade.FadeState.FADE_STATE_IN);
        //fade.FadeIn();
    }

    public override void GoToNextScene()
    {
        //Destroy(canvas);
        SceneManager.LoadScene(nextScene);
    }

    public override void UpdateThisScene()
    {

    }
}
