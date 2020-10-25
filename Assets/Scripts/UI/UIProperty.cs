using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIProperty : MonoBehaviour
{
    [SerializeField] protected RectTransform[] rectTransform = null;
    [SerializeField] [Range(-1.0f, 1.0f)] protected float uiSizeParentCoefficient = 1.0f;
    [SerializeField] [Range(-1.0f, 1.0f)] protected float[] uiSizeChildrenCoefficient = null;
    [SerializeField] [Range(-2.0f, 2.0f)] protected float uiPosXParentCoefficient = 0.0f;
    [SerializeField] [Range(-2.0f, 2.0f)] protected float[] uiPosXChildrenCoefficient = null;
    [SerializeField] [Range(-2.0f, 2.0f)] protected float uiPosYParentCoefficient = 0.0f;
    [SerializeField] [Range(-2.0f, 2.0f)] protected float[] uiPosYChildrenCoefficient = null;
    [SerializeField] protected Vector2[] sizeCoefficient = null;

    public enum PositionMode
    {
        POSITION_MODE_SET,
        POSITION_MODE_ORDER,
    }

    [SerializeField] protected PositionMode positionMode = PositionMode.POSITION_MODE_SET;

    public enum OrderMode
    {
        ORDER_MODE_HORIZONTAL,
        ORDER_MODE_VERTICAL,
        ORDER_MODE_NONE
    }

    [SerializeField] protected OrderMode orderMode = OrderMode.ORDER_MODE_NONE;

    public enum OrderReference
    {
        ORDER_REFERENCE_SCREEN,
        ORDER_REFERENCE_SELF,
        ORDER_REFERENCE_NONE
    }

    [SerializeField] protected OrderReference orderReference = OrderReference.ORDER_REFERENCE_NONE;
    [SerializeField] [Range(-2.0f, 2.0f)] private float spaceCoefficient = 0.0f;

    protected virtual void Awake()
    {
        sizeCoefficient = new Vector2[rectTransform.Length];

        for (int i = 0; i < rectTransform.Length; i++)
        {
            sizeCoefficient[i] = new Vector2(rectTransform[i].sizeDelta.x / rectTransform[i].sizeDelta.y, rectTransform[i].sizeDelta.y / rectTransform[i].sizeDelta.x);
        }
    }

    // Widthによる拡大縮小
    public virtual void SetUISizeMatchX()
    {
        float size_x = Screen.width * uiSizeParentCoefficient;

        for (int i = 0; i < rectTransform.Length; i++)
        {
            rectTransform[i].sizeDelta = new Vector2(size_x, size_x * sizeCoefficient[i].y) * uiSizeChildrenCoefficient[i];
        }
    }

    // Heightによる拡大縮小
    public virtual void SetUISizeMatchY()
    {
        float size_y = Screen.height * uiSizeParentCoefficient;

        for (int i = 0; i < rectTransform.Length; i++)
        {
            rectTransform[i].sizeDelta = new Vector2(size_y * sizeCoefficient[i].x, size_y) * uiSizeChildrenCoefficient[i];
        }
    }

    public void SetUIPosition()
    {
        switch (positionMode)
        {
            case PositionMode.POSITION_MODE_SET:
                PositionModeSet();
                break;
            case PositionMode.POSITION_MODE_ORDER:
                PositionModeOrder(spaceCoefficient, orderMode);
                break;
            default:
                break;
        }
    }

    public void PositionModeSet()
    {
        rectTransform[0].anchoredPosition = new Vector2(
            Screen.width / 2 * uiPosXParentCoefficient * uiPosXChildrenCoefficient[0],
            Screen.height / 2 * uiPosYParentCoefficient * uiPosYChildrenCoefficient[0]);

        for (int i = 1; i < rectTransform.Length; i++)
        {
            rectTransform[i].anchoredPosition = new Vector2(
                    rectTransform[0].anchoredPosition.x * uiPosXChildrenCoefficient[i],
                    rectTransform[0].anchoredPosition.y * uiPosYChildrenCoefficient[i]);
        }
    }

    public void PositionModeOrder(float coefficient, OrderMode order_mode)
    {
        PositionModeSet();

        switch (order_mode)
        {
            case OrderMode.ORDER_MODE_HORIZONTAL:
                OrherModeHorizontal(coefficient);
                break;
            case OrderMode.ORDER_MODE_VERTICAL:
                OrherModeVertical(coefficient);
                break;
            default:
                break;
        }
    }

    private void OrherModeHorizontal(float coefficient)
    {
        float space = GetOrderReferenceSpace().x * coefficient;

        for (int i = 1; i < rectTransform.Length; i++)
        {
            float pos_x = rectTransform[0].position.x + i * space;
            rectTransform[i].position = new Vector3(pos_x, rectTransform[i].position.y, rectTransform[i].position.z);
        }
    }

    private void OrherModeVertical(float coefficient)
    {
        float space = GetOrderReferenceSpace().y * coefficient;

        for (int i = 1; i < rectTransform.Length; i++)
        {
            float pos_y = rectTransform[0].position.y + i * space;
            rectTransform[i].position = new Vector3(rectTransform[i].position.x, pos_y, rectTransform[i].position.z);
        }
    }

    public Vector2 GetOrderReferenceSpace()
    {
        switch(orderReference)
        {
            case OrderReference.ORDER_REFERENCE_SCREEN:
                return ScreenInfo.screenSize;
            case OrderReference.ORDER_REFERENCE_SELF:
                return rectTransform[0].sizeDelta / 100;
            default:
                return Vector2.zero;
        }
    }
}
