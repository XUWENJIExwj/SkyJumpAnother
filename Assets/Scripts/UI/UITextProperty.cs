using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextProperty : UIProperty
{
    [SerializeField] private Text text = null;
    [SerializeField] private int fontDefaultSize = 56;
    [SerializeField] private float fontSizeCoefficient = 0.0f;

    private void Start()
    {
        fontSizeCoefficient = fontDefaultSize / ScreenInfo.screenOriginSize.x;
    }

    // Widthによる拡大縮小
    public override void SetUISizeXC()
    {
        float size_x = Screen.width * uiSizeCoefficient;

        for (int i = 0; i < rectTransform.Length; i++)
        {
            rectTransform[i].sizeDelta = new Vector2(size_x, size_x * sizeCoefficient[i].y) * uiChildrenSizeCoefficient[i];
        }
        SetFontSize();
    }

    // Heightによる拡大縮小
    public override void SetUISizeYC()
    {
        float size_y = Screen.height * uiSizeCoefficient;

        for (int i = 0; i < rectTransform.Length; i++)
        {
            rectTransform[i].sizeDelta = new Vector2(size_y * sizeCoefficient[i].x, size_y) * uiChildrenSizeCoefficient[i];
        }
        SetFontSize();
    }

    private void SetFontSize()
    {
        text.fontSize = (int)Mathf.Ceil(fontSizeCoefficient * rectTransform[0].sizeDelta.x);
    }
}
