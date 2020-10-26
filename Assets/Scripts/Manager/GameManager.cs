using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : CommonManager
{
    [SerializeField] private CameraBehaviour cameraBehaviour = null;
    [SerializeField] private ObjectWithFlick player = null;
    [SerializeField] private Trajectory trajectory = null;
    [SerializeField] private Score score = null;

    [SerializeField] private BGBehaviour[] bgBehaviours = null;
    [SerializeField] private LineCreator lineCreator = null;

    static public int bgNum { get; private set; }

    [SerializeField] private int oldStep = 0;
    [SerializeField] private int step = 0;

    protected override void Awake()
    {
        base.Awake();
        nextScene = "Title";
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

        lineCreator.UpdateLine(cam.transform.position.y);

        cameraBehaviour.UpdateCamera(player.gameObject.transform.position.y);

        if (player.GetIfGameOver() && fade.GetFadeState() == Fade.FadeState.FADE_STATE_NONE)
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
        RankInfo.SetNewRankInfo("Player", score.GetScore());
        fade.SetFadeState(Fade.FadeState.FADE_STATE_OUT);
        fade.FadeOut();
        AudioManager.PlaySE(AudioManager.AudioSourceIndex.AUDIO_SOURCE_SE_TYPE_A, AudioManager.SE.SE_GAME_OVER, 0.2f);
    }
}