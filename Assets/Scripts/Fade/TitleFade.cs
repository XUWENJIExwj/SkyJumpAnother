using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFade : Fade
{
    public override void FadeIn()
    {
        fadeImage.color = new Vector4(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a - fadeSpeed);

        if (fadeImage.color.a <= 0.0f)
        {
            fadeState = FadeState.FADE_STATE_NONE;
            fadeImage.gameObject.SetActive(false);
        }
    }

    public override void FadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        for (int i = 0; i < uiParts.Length - 1; i++)
        {
            uiParts[i].anchoredPosition = new Vector2(uiParts[i].anchoredPosition.x, uiParts[i].anchoredPosition.y + uiPartsSpeed);
        }
        uiParts[uiParts.Length - 1].anchoredPosition = new Vector2(uiParts[uiParts.Length - 1].anchoredPosition.x, uiParts[uiParts.Length - 1].anchoredPosition.y - uiPartsSpeed);

        fadeImage.color = new Vector4(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a + fadeSpeed);
        bgSprite.color = new Vector4(bgSprite.color.r - fadeSpeed, bgSprite.color.g - fadeSpeed, bgSprite.color.b - fadeSpeed, bgSprite.color.a);

        if (fadeImage.color.a >= 1.0f)
        {
            fadeState = FadeState.FADE_STATE_NEXT_SCENE;
        }
    }
}
