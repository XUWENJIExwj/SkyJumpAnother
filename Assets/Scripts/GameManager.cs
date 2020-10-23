using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ObjectCreator objectCreator;
    public ObjectWithFlick playerObjWithFlick;
    public GameObject audioManagerPrefab;
    public AudioManager audioManager;
    public GameObject skinSupportPrefab;
    public SkinInfo skinSupport;
    public CanvasManager canvasManager;

    public GameObject score;
    public GameObject scoreFrame;

    public GameFade fade;

    // Start is called before the first frame update
    void Awake()
    {
        // AudioManagerへのアタッチ
        if (!GameObject.Find("AudioManager"))
        {
            Instantiate(audioManagerPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).name = "AudioManager";
        }
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        objectCreator.audioManager = audioManager;
        playerObjWithFlick.audioManager = audioManager;

        // SkinManagerへのアタッチ
        if (!GameObject.Find("SkinSupport"))
        {
            Instantiate(skinSupportPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).name = "SkinSupport";
        }
        skinSupport = GameObject.Find("SkinSupport").GetComponent<SkinInfo>();
        //playerObjWithFlick.GetComponent<SpriteRenderer>().color = skinSupport.GetPlayerColor();

        // BGMの再生
        //audioManager.PlayBGM(AudioManager.BGM.BGM_GAME);

        canvasManager.SetScorePosition(Screen.width / 2 - 300.0f, Screen.height / 2 * 0.9f);

        scoreFrame.transform.localPosition = score.transform.localPosition;
        scoreFrame.transform.localScale = score.transform.localScale;

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
                SceneManager.LoadScene("Result");
                break;
            default:
                if (playerObjWithFlick.GetIfGameOver())
                {
                    fade.SetFadeState(Fade.FadeState.FADE_STATE_OUT);
                    //audioManager.PlaySE(AudioManager.SE.SE_GAME_OVER, 1, 0.2f);
                }
                break;
        }  
    }
}
