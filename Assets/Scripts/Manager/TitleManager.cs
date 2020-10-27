using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TitleManager : CommonManager
{
    [SerializeField] private GameObject bestMark = null;
    [SerializeField] private Text scoreBest = null;
    [SerializeField] private GameObject particle = null;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        SetBgSize();
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
    }

    public override void PrepareToGoToNextScene(string next_scene)
    {
        Destroy(particle);
        base.PrepareToGoToNextScene(next_scene);
    }
}
