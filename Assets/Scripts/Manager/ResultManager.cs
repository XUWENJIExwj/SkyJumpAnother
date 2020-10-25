using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public float screenWidth;
    public float screenHeight;

    private SpriteRenderer resultSpriteRenderer;

    public GameObject audioManagerPrefab;
    public AudioManager audioManager;
    public GameObject skinSupportPrefab;
    public SkinInfo skinSupport;

    public GameObject canvasOldScene;
    public CanvasManager canvasManager;
    public GameObject scoreDisplay;
    public Score score;
    public Score scoreBest;
    public Score scoreOther;
    public GameObject scoreFrame;
    public GameObject lightObj;
    public float lightSize;
    public bool hasCreatedSoul;
    public GameObject player;
    public GameObject soulPrefab;
    public ResultFade fade;
    public InputField inputer;
    public CheckStringByte checkStringByte;

    public RankInfo rank;

    // Start is called before the first frame update
    void Start()
    {
        screenWidth = (float)Screen.width / 100;
        screenHeight = (float)Screen.height / 100;

        resultSpriteRenderer = GetComponent<SpriteRenderer>();

        resultSpriteRenderer.size = new Vector2(6.4f * screenHeight / 11.36f, screenHeight);

        RectTransform frame = scoreFrame.GetComponent<RectTransform>();

        // AudioManagerへのアタッチ
        if (!GameObject.Find("AudioManager"))
        {
            Instantiate(audioManagerPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).name = "AudioManager";
        }
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        // SkinManagerへのアタッチ
        if (!GameObject.Find("SkinSupport"))
        {
            Instantiate(skinSupportPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).name = "SkinSupport";
        }
        skinSupport = GameObject.Find("SkinSupport").GetComponent<SkinInfo>();

        //player.GetComponent<SpriteRenderer>().color = new Vector4(
        //    skinSupport.GetPlayerColor().r * 140 / 255.0f,
        //    skinSupport.GetPlayerColor().g * 140 / 255.0f,
        //    skinSupport.GetPlayerColor().b * 140 / 255.0f,
        //    1.0f);

        //player.GetComponent<SpriteRenderer>().color = skinSupport.GetPlayerColor();

        if (canvasOldScene = GameObject.Find("Canvas"))
        {
            score = canvasOldScene.GetComponentInChildren<Score>();
            Destroy(canvasOldScene);
        }
        else
        {
            scoreOther.gameObject.SetActive(true);
            score = scoreOther;
            score.tag = "Score";
            //score.score = 100;
        }

        score.transform.SetParent(scoreDisplay.transform);

        //scoreBest.scoreIndex = score.scoreIndex;

        canvasManager.SetScorePosition(-40.0f, frame.anchoredPosition.y - 15.0f);
        canvasManager.SetScoreSize(0.65f, 0.65f);

        //rank.LoadRankBinary();

        //rank.LoadRank();

        //if (rank.CheckIfRankIn(score.score))
        //{
        //    inputer.gameObject.SetActive(true);
        //    // 入力処理
        //    rank.SetNewRankInfo("Player", score.score);
        //    rank.SortRank();
        //    //rank.SortRankBinary();
        //}

        //scoreBest.SetScore((float)rank.LoadChampionInfo().score / score.scoreIndex);

        canvasManager.SetScoreBestPosition(45.0f, frame.anchoredPosition.y - 175.0f);
        canvasManager.SetScoreBestSize(0.48f, 0.48f);

        // BGMの再生
        //audioManager.PlayBGM(AudioManager.BGM.BGM_RESULT);

        hasCreatedSoul = false;

        fade.SetFadeState(Fade.FadeState.FADE_STATE_IN);
    }

    private void FixedUpdate()
    {
        switch (fade.GetFadeState())
        {
            case Fade.FadeState.FADE_STATE_IN:
                fade.FadeIn();
                break;
            case Fade.FadeState.FADE_STATE_OUT:
                fade.FadeOut();
                break;
            case Fade.FadeState.FADE_STATE_NEXT_SCENE:
                Destroy(canvasManager.gameObject);
                SceneManager.LoadScene("Title");
                break;
            default:
                SetLightSize();

                if (lightObj.transform.localScale.x >= 1.0f && !hasCreatedSoul)
                {
                    CreateSoul();
                }
                break;
        }
    }

    public void InputNameOK()
    {
        //int idx = rank.GetNewRankInfo().rank - 1;
        //rank.SetNewRankName(inputer.textComponent.text, idx);

        //checkStringByte.MakeCharacterToLimit(rank.GetRankInfo(idx).name);

        //bool all_blank = true;

        //for (int i = 0; i < inputer.textComponent.text.Length; i++)
        //{
        //    if (inputer.textComponent.text[i] != 0x20)
        //    {
        //        all_blank = false;
        //        break;
        //    }
        //}

        //if(all_blank)
        //{
        //    rank.SetNewRankName("Player", idx);
        //}

        //rank.SaveRank(idx);
        //rank.SaveRankBinary();

        //audioManager.PlaySE(AudioManager.SE.SE_RESULT, 1, 0.5f);
        inputer.gameObject.SetActive(false);
    }

    public void InputNameCancel()
    {
        inputer.textComponent.text = "";

        //audioManager.PlaySE(AudioManager.SE.SE_RESULT, 1, 0.5f);
        inputer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void GoToTitleScene()
    {
        InputNameCancel();
        fade.SetFadeState(Fade.FadeState.FADE_STATE_OUT);
    }

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

//    public void SaveScore(string s)
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
        
//        StreamWriter sw = new StreamWriter(path, false); //true=追記 false=上書き
//        sw.WriteLine(s);
//        sw.Flush();
//        sw.Close();
//    }

    public void SetLightSize()
    {
        if (lightObj.transform.localScale.x < 1.0f && Time.fixedTime >= 0.3f)
        {
            lightObj.transform.localScale = new Vector3(lightObj.transform.localScale.x + lightSize, lightObj.transform.localScale.y, lightObj.transform.localScale.z);
        }
    }

    public void CreateSoul()
    {
        hasCreatedSoul = true;
        Vector3 pos = new Vector3(-0.6f, -3.1f, -2.0f);
        GameObject soul = Instantiate(soulPrefab, pos, Quaternion.identity);
        Animation animation = soul.GetComponent<Animation>();
        //string clip = "Soul0" + skinSupport.GetPlayerType().ToString();
        //animation.clip = animation.GetClip(clip);
        //animation.Play();
    }
}
