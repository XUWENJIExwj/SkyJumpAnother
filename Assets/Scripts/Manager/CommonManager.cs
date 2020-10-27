using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class CommonManager : MonoBehaviour
{
    [SerializeField] protected Camera cam = null;
    [SerializeField] protected SpriteRenderer bg = null;
    [SerializeField] protected Fade fade;
    [SerializeField] protected UIProperty[] uiPartsMatchX = null; // Widthによる拡大縮小
    [SerializeField] protected UIProperty[] uiPartsMatchY = null; // Heightによる拡大縮小
    [SerializeField] protected ObjectProperty[] objectProperties = null;

    protected string nextScene;

    protected virtual void Awake()
    {
        cam.orthographicSize = ScreenInfo.cameraOrthographicSize;
    }

    protected virtual void Start()
    {
        fade.SetFadeState(Fade.FadeState.FADE_STATE_IN);
        fade.FadeIn();

        InitUI();
    }

    private void FixedUpdate()
    {
        switch (fade.GetFadeState())
        {
            case Fade.FadeState.FADE_STATE_IN:
            case Fade.FadeState.FADE_STATE_OUT:
                break;
            case Fade.FadeState.FADE_STATE_NEXT_SCENE:
                GoToNextScene();
                break;
            default:
                UpdateThisScene();
                break;
        }
    }

    private void Update()
    {
        // 完成後削除
        InitUI();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerPrefs.DeleteAll();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            RankInfo.SetOneScore();
        } 
    }

    public virtual void GoToNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    public virtual void UpdateThisScene()
    {

    }

    public void SetBgSize()
    {
        bg.size = ScreenInfo.bgSizeMatchY;
    }

    public virtual void PrepareToGoToNextScene(string next_scene)
    {
        if (fade.GetFadeState() == Fade.FadeState.FADE_STATE_NONE)
        {
            nextScene = next_scene;
            fade.SetFadeState(Fade.FadeState.FADE_STATE_OUT);
            fade.FadeOut();
            AudioManager.PlaySE(AudioManager.AudioSourceIndex.AUDIO_SOURCE_SE_TYPE_A, AudioManager.SE.SE_TITLE, 0.5f);
        }
    }

    public void InitUI()
    {
        if(uiPartsMatchX != null)
        {
            foreach (UIProperty ui_property in uiPartsMatchX)
            {
                ui_property.SetUISizeMatchX();
                ui_property.SetUIPosition();
            }
        }

        if (uiPartsMatchY != null)
        {
            foreach (UIProperty ui_property in uiPartsMatchY)
            {
                ui_property.SetUISizeMatchY();
                ui_property.SetUIPosition();
            }
        }

        if (objectProperties != null)
        {
            foreach (ObjectProperty obj_property in objectProperties)
            {
                obj_property.SetSize();
                obj_property.SetPos();
            }
        }
    }
}
