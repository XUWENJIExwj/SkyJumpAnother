using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : CommonManager
{
    //public GameObject canvas;

    [SerializeField] private GameObject bestMark = null;
    [SerializeField] private Text scoreBest = null;

    //public GameObject particle;

    protected override void Awake()
    {
        base.Awake();
        //rank.LoadRankBinary();
    }

    protected override void Start()
    {
        DisplayBestScore();
        AudioManager.PlayBGM(AudioManager.BGM.BGM_TITLE);
        base.Start();
    }

    public void DisplayBestScore()
    {
        RankInfo.LoadRank();

        if (RankInfo.LoadChampionInfo().rank != 0)
        {
            bestMark.SetActive(true);
            scoreBest.gameObject.SetActive(true);
            scoreBest.text = "Best Score : " + RankInfo.LoadChampionInfo().score.ToString() + " m";
        }
        else
        {
            bestMark.SetActive(false);
            scoreBest.gameObject.SetActive(false);
        }

        //RankInfo.RankDebug();
    }

    public override void GoToNextScene()
    {
        //Destroy(canvas);
        //Destroy(particle);
        SceneManager.LoadScene(nextScene);
    }

    public override void UpdateThisScene()
    {

    }
}
