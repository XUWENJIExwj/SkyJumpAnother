using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProperty : MonoBehaviour
{
    [SerializeField] private Vector2 objSize = Vector2.zero;
    [SerializeField] private Vector2 objHalfSize = Vector2.zero;
    [SerializeField] private Vector2 objSizeDefault = Vector2.zero;
    [SerializeField] private Vector2 objSizeDeviation = Vector2.zero;
    [SerializeField] private Vector2 sizeSliced = Vector2.one;
    [SerializeField] private Vector3 posParentCoefficient = Vector3.zero;

    private Vector3 work;

    public struct ObjBorder
    {
        public float top;
        public float bottom;
        public float left;
        public float right;
    }

    [SerializeField] private ObjBorder objBorder;

    private void Awake()
    {
        SpriteRenderer spriteRenderer = null;

        if (TryGetComponent(out spriteRenderer))
        {
            sizeSliced = spriteRenderer.size;
        }

        SetSize(new Vector3(objSizeDefault.x, objSizeDefault.y, 1.0f));
    }

    public void SetSize()
    {
        SetSize(new Vector3(objSizeDefault.x, objSizeDefault.y, 1.0f));
    }

    public void SetSize(Vector3 size)
    {
        float size_x = size.x * ScreenInfo.screenCoefficient.x;
        objSize = new Vector2(size_x, size_x * size.y / size.x);
        objHalfSize = objSize / 2 - objSizeDeviation * ScreenInfo.screenCoefficient.x;
        objHalfSize *= sizeSliced;
        transform.localScale = new Vector3(objSize.x, objSize.y, 1.0f);
    }

    public Vector2 GetObjSize()
    {
        return objSize;
    }

    public Vector2 GetObjHalfSize()
    {
        return objHalfSize;
    }

    public ObjBorder GetObjBorder()
    {
        objBorder.top = transform.position.y + objHalfSize.y;
        objBorder.bottom = transform.position.y - objHalfSize.y;
        objBorder.left = transform.position.x - objHalfSize.x;
        objBorder.right = transform.position.x + objHalfSize.x;

        return objBorder;
    }

    public void SetPos()
    {
        transform.position = new Vector3(
            ScreenInfo.screenHalfSize.x * posParentCoefficient.x,
            ScreenInfo.screenHalfSize.y * posParentCoefficient.y,
            transform.position.z);
    }
}
