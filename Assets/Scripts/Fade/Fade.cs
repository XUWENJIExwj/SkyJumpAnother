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
    [SerializeField] [Range(0.0f, 20.0f)] protected float uiPartsMoveY = 0.0f;

    public enum FadeState
    {
        FADE_STATE_NONE,
        FADE_STATE_IN,
        FADE_STATE_OUT,
        FADE_STATE_NEXT_SCENE
    }

    [SerializeField] protected FadeState fadeState;

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
