using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextProperty : UIProperty
{
    [SerializeField] private Text[] text = null;
    [SerializeField] private int fontSizeDefault = 56;
    [SerializeField] private Vector2 fontSizeParentCoefficient = Vector2.zero;
    [SerializeField] [Range(0.0f, 1.0f)] private float[] fontSizeChildrenCoefficient = null;

    protected override void Awake()
    {
        base.Awake();
        fontSizeParentCoefficient.x = fontSizeDefault / ScreenInfo.screenDefaultSize.x;
        fontSizeParentCoefficient.y = fontSizeDefault / ScreenInfo.screenDefaultSize.y;
    }

    // Widthによる拡大縮小
    public override void SetUISizeMatchX()
    {
        base.SetUISizeMatchX();

        SetFontSizeMatchX();
    }

    // Heightによる拡大縮小
    public override void SetUISizeMatchY()
    {
        base.SetUISizeMatchY();

        SetFontSizeMatchY(); 
    }

    private void SetFontSizeMatchX()
    {
        for (int i = 0; i < text.Length; i++)
        {
            text[i].fontSize = (int)Mathf.Ceil(fontSizeParentCoefficient.x * rectTransform[i].sizeDelta.x * fontSizeChildrenCoefficient[i]);
        }
    }

    private void SetFontSizeMatchY()
    {
        for (int i = 0; i < text.Length; i++)
        {
            text[i].fontSize = (int)Mathf.Ceil(fontSizeParentCoefficient.y * rectTransform[i].sizeDelta.y * fontSizeChildrenCoefficient[i]);
        }
    }
}
