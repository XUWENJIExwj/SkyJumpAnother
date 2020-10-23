using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextProperty : UIProperty
{
    [SerializeField] private Text[] text = null;
    [SerializeField] private int fontDefaultSize = 56;
    [SerializeField] private float fontSizeParentCoefficient = 0.0f;
    [SerializeField] [Range(0.0f, 1.0f)] private float[] fontSizeChildrenCoefficient = null;
    [SerializeField] [Range(0.0f, 1.0f)] private float textSpaceCoefficient = 0.0f;

    protected override void Awake()
    {
        base.Awake();
        fontSizeParentCoefficient = fontDefaultSize / ScreenInfo.screenOriginSize.x;
    }

    // Widthによる拡大縮小
    public override void SetUISizeXC()
    {
        float size_x = Screen.width * uiSizeParentCoefficient;

        for (int i = 0; i < rectTransform.Length; i++)
        {
            rectTransform[i].sizeDelta = new Vector2(size_x, size_x * sizeCoefficient[i].y) * uiSizeChildrenCoefficient[i];
        }

        SetFontSize();
        SetChildrenPos();
    }

    // Heightによる拡大縮小
    public override void SetUISizeYC()
    {
        float size_y = Screen.height * uiSizeParentCoefficient;

        for (int i = 0; i < rectTransform.Length; i++)
        {
            rectTransform[i].sizeDelta = new Vector2(size_y * sizeCoefficient[i].x, size_y) * uiSizeChildrenCoefficient[i];
        }

        SetFontSize();
        SetChildrenPos();
    }

    private void SetFontSize()
    {
        for (int i = 0; i < text.Length; i++)
        {
            text[i].fontSize = (int)Mathf.Ceil(fontSizeParentCoefficient * rectTransform[i].sizeDelta.x * fontSizeChildrenCoefficient[i]);
        }
    }

    private void SetChildrenPos()
    {
        float text_space = ScreenInfo.screenSize.y * textSpaceCoefficient;

        for (int i = 1; i < rectTransform.Length; i++)
        {
            float pos_y = rectTransform[0].position.y - i * text_space;
            rectTransform[i].position = new Vector3(rectTransform[i].position.x, pos_y, rectTransform[i].position.z);
        }
    }
}
