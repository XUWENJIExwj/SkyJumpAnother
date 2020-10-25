using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextProperty : UIProperty
{
    [SerializeField] private Text[] text = null;
    [SerializeField] private int fontSizeDefault = 56;
    [SerializeField] private float fontSizeParentCoefficient = 0.0f;
    [SerializeField] [Range(0.0f, 1.0f)] private float[] fontSizeChildrenCoefficient = null;

    protected override void Awake()
    {
        base.Awake();
        fontSizeParentCoefficient = fontSizeDefault / ScreenInfo.screenDefaultSize.x;
    }

    // Widthによる拡大縮小
    public override void SetUISizeMatchX()
    {
        base.SetUISizeMatchX();

        SetFontSize();
    }

    // Heightによる拡大縮小
    public override void SetUISizeMatchY()
    {
        base.SetUISizeMatchY();

        SetFontSize(); 
    }

    private void SetFontSize()
    {
        for (int i = 0; i < text.Length; i++)
        {
            text[i].fontSize = (int)Mathf.Ceil(fontSizeParentCoefficient * rectTransform[i].sizeDelta.x * fontSizeChildrenCoefficient[i]);
        }
    }
}
