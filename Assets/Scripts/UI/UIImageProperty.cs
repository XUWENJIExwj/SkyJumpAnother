using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIImageProperty : UIProperty
{
    // Widthによる拡大縮小
    public override void SetUISizeXC()
    {
        float size_x = Screen.width * uiSizeParentCoefficient;

        for (int i = 0; i < rectTransform.Length; i++)
        {
            rectTransform[i].sizeDelta = new Vector2(size_x, size_x * sizeCoefficient[i].y) * uiSizeChildrenCoefficient[i];
        }
    }

    // Heightによる拡大縮小
    public override void SetUISizeYC()
    {
        float size_y = Screen.height * uiSizeParentCoefficient;

        for (int i = 0; i < rectTransform.Length; i++)
        {
            rectTransform[i].sizeDelta = new Vector2(size_y * sizeCoefficient[i].x, size_y) * uiSizeChildrenCoefficient[i];
        }
    }
}