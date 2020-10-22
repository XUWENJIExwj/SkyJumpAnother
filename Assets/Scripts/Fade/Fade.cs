using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Fade : MonoBehaviour
{
    [SerializeField] protected Image fadeImage = null;
    [SerializeField] protected float fadeFrame = 0.0f;
    [SerializeField] protected float fadeSpeed = 0.0f;
    [SerializeField] protected RectTransform[] uiParts = null;
    [SerializeField] protected float uiPartsSpeed = 0.0f;
    [SerializeField] protected SpriteRenderer bgSprite;

    public enum FadeState
    {
        FADE_STATE_NONE,
        FADE_STATE_IN,
        FADE_STATE_OUT,
        FADE_STATE_NEXT_SCENE
    }

    [SerializeField] protected FadeState fadeState;

    private void Start()
    {
        fadeSpeed = 1.0f / fadeFrame;
    }

    public abstract void FadeIn();

    public abstract void FadeOut();

    public void SetFadeState(FadeState fade_state)
    {
        fadeState = fade_state;
    }

    public FadeState GetFadeState()
    {
        return fadeState;
    }
}
