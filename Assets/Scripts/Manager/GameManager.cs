using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : CommonManager
{
    [SerializeField] private ObjectWithFlick player;
    [SerializeField] private CameraBehaviour cameraBehaviour;

    [SerializeField] private BGBehaviour[] bgBehaviours = null;

    //public ObjectCreator objectCreator;
    //public CanvasManager canvasManager;

    //public GameObject score;
    //public GameObject scoreFrame;

    //void Awake()
    //{
        //playerObjWithFlick.GetComponent<SpriteRenderer>().color = skinSupport.GetPlayerColor();

        // BGMの再生
        //audioManager.PlayBGM(AudioManager.BGM.BGM_GAME);

        //canvasManager.SetScorePosition(Screen.width / 2 - 300.0f, Screen.height / 2 * 0.9f);

        //scoreFrame.transform.localPosition = score.transform.localPosition;
        //scoreFrame.transform.localScale = score.transform.localScale;

    //}

    protected override void Awake()
    {
        base.Awake();
        //rank.LoadRankBinary();
    }

    protected override void Start()
    {
        AudioManager.PlayBGM(AudioManager.BGM.BGM_GAME);
        base.Start();

        // BGの初期化
        for (int i = 0; i < bgBehaviours.Length; i++)
        {
            bgBehaviours[i].InitBg(i);
        }
    }

    public override void GoToNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    public override void UpdateThisScene()
    {
        //cameraBehaviour.UpdateCamera(player.gameObject.transform.position.y);

        //if(player.GetIfGameOver())
        //{
        //    fade.SetFadeState(Fade.FadeState.FADE_STATE_OUT);
        //    AudioManager.PlaySE(AudioManager.AudioSourceIndex.AUDIO_SOURCE_SE_TYPE_A ,AudioManager.SE.SE_GAME_OVER, 0.2f);
        //}
    }
}
