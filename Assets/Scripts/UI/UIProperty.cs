using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIProperty : MonoBehaviour
{
    [SerializeField] protected RectTransform[] rectTransform = null;
    [SerializeField] [Range(-1.0f, 1.0f)] protected float uiSizeParentCoefficient = 1.0f;
    [SerializeField] [Range(-1.0f, 1.0f)] protected float[] uiSizeChildrenCoefficient = null;
    [SerializeField] [Range(-2.0f, 2.0f)] protected float uiPosXCoefficient = 0.0f;
    [SerializeField] [Range(-2.0f, 2.0f)] protected float uiPosYCoefficient = 0.0f;
    [SerializeField] protected Vector2[] sizeCoefficient = null;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        sizeCoefficient = new Vector2[rectTransform.Length];

        for (int i = 0; i < rectTransform.Length; i++)
        {
            sizeCoefficient[i] = new Vector2(rectTransform[i].sizeDelta.x / rectTransform[i].sizeDelta.y, rectTransform[i].sizeDelta.y / rectTransform[i].sizeDelta.x);
        }
    }

    // Widthによる拡大縮小
    public abstract void SetUISizeXC();

    // Heightによる拡大縮小
    public abstract void SetUISizeYC();

    public void SetUIPosition()
    {
        rectTransform[0].anchoredPosition = new Vector2(Screen.width / 2 * uiPosXCoefficient, Screen.height / 2 * uiPosYCoefficient);
    }
}
