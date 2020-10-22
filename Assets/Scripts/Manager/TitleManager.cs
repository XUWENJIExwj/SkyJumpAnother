using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TitleManager : CommonManager
{


    //private SpriteRenderer titleSpriteRenderer;

    //public GameObject audioManagerPrefab;
    //public AudioManager audioManager;

    //public GameObject skinSupportPrefab;
    //public SkinSupport skinSupport;
    //public SkinManager skinManager;

    //public GameObject canvas;
    //public Rank rank;
    //public Text scoreBest;
    //public GameObject best;
    //public TitleFade fade;

    //public GameObject particle;

    //public bool deleteRank = false;

    // Start is called before the first frame update
    void Awake()
    {
        

        
        //titleBg.sizeDelta= ScreenInfo.bgSizeYC * 100;

        //float title_logo_x = canvasScaler.referenceResolution.x * 0.9f;
        //float title_logo_yc = titleLogo.sizeDelta.y / titleLogo.sizeDelta.x;
        //titleLogo.sizeDelta = new Vector2(title_logo_x, title_logo_x * title_logo_yc);
        //titleLogo.sizeDelta=
        //titleLogo.position = new Vector3(titleLogo.position.x, Screen.height / 2 - 250.0f, titleLogo.position.z);

        //Screen.SetResolution(1080, 1920, false);


        //titleSpriteRenderer = GetComponent<SpriteRenderer>();

        //titleSpriteRenderer.size = new Vector2(6.4f * screenHeight / 11.36f, screenHeight);

        //// AudioManagerへのアタッチ
        //if (!GameObject.Find("AudioManager"))
        //{
        //    Instantiate(audioManagerPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).name = "AudioManager";
        //}
        //audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        //skinManager.SetAudioManager(audioManager);

        //// SkinManagerへのアタッチ
        //if (!GameObject.Find("SkinSupport"))
        //{
        //    Instantiate(skinSupportPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).name = "SkinSupport";
        //}
        //skinSupport = GameObject.Find("SkinSupport").GetComponent<SkinSupport>();
        //skinManager.SetSkinSupport(skinSupport);
        //skinManager.InitInTitleScene();

        //// BGMの再生
        //audioManager.PlayBGM(AudioManager.BGM.BGM_TITLE);

        //if (deleteRank)
        //{
        //    PlayerPrefs.DeleteAll();
        //}

        //rank.LoadRank();
        ////rank.LoadRankBinary();

        //if (rank.LoadChampionInfo().rank != 0)
        //{
        //    best.SetActive(true);
        //    scoreBest.gameObject.SetActive(true);
        //    scoreBest.text = "Best Score : " + rank.LoadChampionInfo().score.ToString() + " m";
        //}
        //else
        //{
        //    best.SetActive(false);
        //    scoreBest.gameObject.SetActive(false);
        //}

        //fade.SetFadeState(Fade.FadeState.FADE_STATE_IN);
    }

    private void Start()
    {
        InitUI();
    }

    private void Update()
    {
        // 完成後削除
        InitUI();
    }

    private void FixedUpdate()
    {
        //switch(fade.GetFadeState())
        //{
        //    case Fade.FadeState.FADE_STATE_IN:
        //        fade.FadeIn();
        //        break;
        //    case Fade.FadeState.FADE_STATE_OUT:
        //        fade.FadeOut();
        //        break;
        //    case Fade.FadeState.FADE_STATE_NEXT_SCENE:
        //        Destroy(canvas);
        //        Destroy(particle);
        //        SceneManager.LoadScene(nextScene);
        //        break;
        //    default:
        //        break;
        //}
    }

    //public void GoToNextScene(string next_scene)
    //{
    //    SceneManager.LoadScene(next_scene);
    //    //if (fade.GetFadeState() == Fade.FadeState.FADE_STATE_NONE)
    //    //{
    //    //    
    //    //    fade.SetFadeState(Fade.FadeState.FADE_STATE_OUT);
    //    //    audioManager.PlaySE(AudioManager.SE.SE_TITLE, 1, 0.5f);
    //    //}
    //}

    public void GoToRankScene()
    {
        //if (fade.GetFadeState() == Fade.FadeState.FADE_STATE_NONE)
        //{
        //    nextScene = "Rank";
        //    fade.SetFadeState(Fade.FadeState.FADE_STATE_OUT);
        //    audioManager.PlaySE(AudioManager.SE.SE_TITLE, 1, 0.5f);
        //}
    }

    public void InitUI()
    {
        foreach (UIProperty ui_property in uiPartsXC)
        {
            ui_property.SetUISizeXC();
            ui_property.SetUIPosition();
        }

        foreach (UIProperty ui_property in uiPartsYC)
        {
            ui_property.SetUISizeYC();
            ui_property.SetUIPosition();
        }
    }
}
