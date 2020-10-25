using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : CommonManager
{
    [SerializeField] private CameraBehaviour cameraBehaviour = null;
    [SerializeField] private ObjectWithFlick player = null;
    [SerializeField] private Trajectory trajectory = null;

    [SerializeField] private BGBehaviour[] bgBehaviours = null;

    static public int bgNum { get; private set; }

    [SerializeField] private int oldStep = 0;
    [SerializeField] private int step = 0;

    //public ObjectCreator objectCreator;
    //public CanvasManager canvasManager;

    //public GameObject score;
    //public GameObject scoreFrame;

    //void Awake()
    //{
    //playerObjWithFlick.GetComponent<SpriteRenderer>().color = skinSupport.GetPlayerColor();

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

        bgNum = bgBehaviours.Length;
    }

    public override void GoToNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    public override void UpdateThisScene()
    {
        trajectory.UpdateDotsNumber(step, oldStep);

        player.UpdatePlayer();

        UpdateStep();

        for (int i = 0; i < bgBehaviours.Length; i++)
        {
            bgBehaviours[i].UpdateBg();
        }

        cameraBehaviour.UpdateCamera(player.gameObject.transform.position.y);

        if (player.GetIfGameOver())
        {
            SetGameOver();
        }
    }

    public void UpdateStep()
    {
        // oldStepの記録
        oldStep = step;

        // 次のstepに入るかをチェック
        if (ScreenInfo.bgSizeMatchX.y * step - ScreenInfo.bgPosYDeviationMatchX < cam.transform.position.y)
        {
            step++;
        }
    }

    public void SetGameOver()
    {
        fade.SetFadeState(Fade.FadeState.FADE_STATE_OUT);
        AudioManager.PlaySE(AudioManager.AudioSourceIndex.AUDIO_SOURCE_SE_TYPE_A, AudioManager.SE.SE_GAME_OVER, 0.2f);
    }
}
