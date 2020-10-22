using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public float screenWidth;
    public float screenHeight;

    private SpriteRenderer titleSpriteRenderer;

    public GameObject audioManagerPrefab;
    public AudioManager audioManager;

    public GameObject skinSupportPrefab;
    public SkinSupport skinSupport;
    public SkinManager skinManager;

    public GameObject canvas;
    public Rank rank;
    public Text scoreBest;
    public GameObject best;
    public TitleFade fade;

    public GameObject particle;

    private string nextScene;

    public bool deleteRank = false;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1080, 1920, false);

        screenWidth = (float)Screen.width / 100;
        screenHeight = (float)Screen.height / 100;

        titleSpriteRenderer = GetComponent<SpriteRenderer>();

        titleSpriteRenderer.size = new Vector2(6.4f * screenHeight / 11.36f, screenHeight);

        // AudioManagerへのアタッチ
        if (!GameObject.Find("AudioManager"))
        {
            Instantiate(audioManagerPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).name = "AudioManager";
        }
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        skinManager.SetAudioManager(audioManager);

        // SkinManagerへのアタッチ
        if (!GameObject.Find("SkinSupport"))
        {
            Instantiate(skinSupportPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).name = "SkinSupport";
        }
        skinSupport = GameObject.Find("SkinSupport").GetComponent<SkinSupport>();
        skinManager.SetSkinSupport(skinSupport);
        skinManager.InitInTitleScene();

        // BGMの再生
        audioManager.PlayBGM(AudioManager.BGM.BGM_TITLE);

        if (deleteRank)
        {
            PlayerPrefs.DeleteAll();
        }

        rank.LoadRank();
        //rank.LoadRankBinary();

        if (rank.LoadChampionInfo().rank != 0)
        {
            best.SetActive(true);
            scoreBest.gameObject.SetActive(true);
            scoreBest.text = "Best Score : " + rank.LoadChampionInfo().score.ToString() + " m";
        }
        else
        {
            best.SetActive(false);
            scoreBest.gameObject.SetActive(false);
        }

        fade.SetFadeState(Fade.FadeState.FADE_STATE_IN);
    }

    private void FixedUpdate()
    {
        switch(fade.GetFadeState())
        {
            case Fade.FadeState.FADE_STATE_IN:
                fade.FadeIn();
                break;
            case Fade.FadeState.FADE_STATE_OUT:
                fade.FadeOut();
                break;
            case Fade.FadeState.FADE_STATE_NEXT_SCENE:
                Destroy(canvas);
                Destroy(particle);
                SceneManager.LoadScene(nextScene);
                break;
            default:
                break;
        }
    }

    public void GoToGameScene()
    {
        if (fade.GetFadeState() == Fade.FadeState.FADE_STATE_NONE)
        {
            nextScene = "Game";
            fade.SetFadeState(Fade.FadeState.FADE_STATE_OUT);
            audioManager.PlaySE(AudioManager.SE.SE_TITLE, 1, 0.5f);
        }
    }

    public void GoToRankScene()
    {
        if (fade.GetFadeState() == Fade.FadeState.FADE_STATE_NONE)
        {
            nextScene = "Rank";
            fade.SetFadeState(Fade.FadeState.FADE_STATE_OUT);
            audioManager.PlaySE(AudioManager.SE.SE_TITLE, 1, 0.5f);
        }
    }

    //    public void CreateScoreFile()
    //    {
    //        string path;
    //        string filename = "/score.txt";

    //        if (Application.isEditor)
    //        {
    //            path = Application.dataPath + filename;
    //        }
    //        else
    //        {
    //#if UNITY_IOS

    //#elif UNITY_ANDROID

    //            path = Application.persistentDataPath + filename;
    //#endif
    //        }

    //        if (!File.Exists(path))
    //        {
    //            StreamWriter sw = new StreamWriter(path, false); //true=追記 false=上書き
    //            sw.WriteLine(5000.ToString());
    //            sw.Flush();
    //            sw.Close();
    //        }
    //    }

//    public int LoadScore()
//    {
//        string path;
//        string filename = "/score.txt";

//        if (Application.isEditor)
//        {
//            path = Application.dataPath + filename;
//        }
//        else
//        {
//#if UNITY_IOS

//#elif UNITY_ANDROID

//            path = Application.persistentDataPath + filename;
//#endif
//        }

//        if (File.Exists(path))
//        {
//            StreamReader sr = new StreamReader(path);
//            int s = int.Parse(sr.ReadLine());
//            sr.Close();
//            return s;
//        }
//        else
//        {
//            return 0;
//        }
//    }
}
