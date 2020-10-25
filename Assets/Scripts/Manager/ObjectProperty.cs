using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProperty : MonoBehaviour
{
    [SerializeField] private Vector2 objSize = Vector2.zero;
    [SerializeField] private Vector2 objHalfSize = Vector2.zero;
    [SerializeField] private Vector2 objSizeDefault = Vector2.zero;
    [SerializeField] private float sizeCoefficient = 0.0f;

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
        sizeCoefficient = objSizeDefault.x / ScreenInfo.bgDefaultSize.x;
        float size_x = sizeCoefficient * ScreenInfo.bgSizeMatchX.x;
        objSize = new Vector2(size_x, size_x * objSizeDefault.y / objSizeDefault.x);
        objHalfSize = objSize / 2;

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
}
