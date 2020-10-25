using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract class Fade : MonoBehaviour
{
    [SerializeField] protected Tween fadeAni;
    [SerializeField] protected Image fadeImage = null;
    [SerializeField] protected float fadeTime = 1.0f;
    [SerializeField] protected RectTransform uiPartMoveUp = null;
    [SerializeField] protected RectTransform uiPartMoveDown = null;
    [SerializeField] protected float uiPartsMoveY = 0.0f;
    [SerializeField] protected float uiPartsMoveYDefault = 20.0f;
    [SerializeField] [Range(0.0f, 10.0f)] protected float uiPartsMoveYCoefficient = 1.0f;

    public enum FadeState
    {
        FADE_STATE_NONE,
        FADE_STATE_IN,
        FADE_STATE_OUT,
        FADE_STATE_NEXT_SCENE
    }

    [SerializeField] protected FadeState fadeState;

    private void Awake()
    {
        uiPartsMoveY = uiPartsMoveYDefault / ScreenInfo.screenDefaultSize.y * Screen.height * uiPartsMoveYCoefficient;
    }

    //public abstract void FadeIn();

    //public abstract void FadeOut();

    public void FadeIn()
    {
        fadeImage.DOFade(0, fadeTime).OnComplete(() => FadeInEnd());
    }

    public void FadeOut()
    {
        if (uiPartMoveUp != null)
        {
            uiPartMoveUp.DOMoveY(uiPartMoveUp.position.y + uiPartsMoveY, fadeTime);
        }

        if (uiPartMoveDown != null)
        {
            uiPartMoveDown.DOMoveY(uiPartMoveDown.position.y - uiPartsMoveY, fadeTime);
        }

        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1, fadeTime).OnComplete(() => FadeOutEnd());
    }

    public void SetFadeState(FadeState fade_state)
    {
        fadeState = fade_state;
    }

    public FadeState GetFadeState()
    {
        return fadeState;
    }

    public void FadeInEnd()
    {
        fadeState = FadeState.FADE_STATE_NONE;
        fadeImage.gameObject.SetActive(false);
    }

    public void FadeOutEnd()
    {
        fadeState = FadeState.FADE_STATE_NEXT_SCENE;
    }
}
