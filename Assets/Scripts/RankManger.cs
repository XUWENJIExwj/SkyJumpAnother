using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankManger : MonoBehaviour
{
    public float screenWidth;
    public float screenHeight;
    private SpriteRenderer rankSpriteRenderer;

    public GameObject audioManagerPrefab;
    public AudioManager audioManager;

    public GameObject canvas;
    public Rank rank;
    public RankFade fade;

    public Text[] rankText;
    public Text[] nameText;
    public Text[] scoreText;

    // Start is called before the first frame update
    void Start()
    {
        screenWidth = (float)Screen.width / 100;
        screenHeight = (float)Screen.height / 100;

        rankSpriteRenderer = GetComponent<SpriteRenderer>();

        rankSpriteRenderer.size = new Vector2(6.4f * screenHeight / 11.36f, screenHeight);

        // AudioManagerへのアタッチ
        if (!GameObject.Find("AudioManager"))
        {
            Instantiate(audioManagerPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).name = "AudioManager";
        }
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        // BGMの再生
        //audioManager.PlayBGM(AudioManager.BGM.BGM_TITLE);

        rank.LoadRank();
        //rank.LoadRankBinary();

        for (int i = 0; i < 5; i++)
        {
            rankText[i].text = (i + 1).ToString();

            if (rank.GetRankInfo(i).rank != 0)
            {
                nameText[i].text = rank.GetRankInfo(i).name;
                scoreText[i].text = rank.GetRankInfo(i).score.ToString();
            }
            else
            {
                nameText[i].text = "---------";
                scoreText[i].text = "-----";
            }
        }

        fade.SetFadeState(Fade.FadeState.FADE_STATE_IN);
    }

    // Update is called once per frame
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
                Destroy(canvas);
                SceneManager.LoadScene("Title");
                break;
            default:
                break;
        }
    }

    public void GoToTitleScene()
    {
        fade.SetFadeState(Fade.FadeState.FADE_STATE_OUT);
        //audioManager.PlaySE(AudioManager.SE.SE_RESULT, 1, 0.5f);
    }
}
