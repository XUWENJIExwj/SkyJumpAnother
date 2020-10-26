using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectCreator : MonoBehaviour
{
    [SerializeField] protected ObjectWithFlick player = null;
    [SerializeField] protected Trajectory trajectory = null;
    [SerializeField] protected GameObject[] objPrefabs = null;
    [SerializeField] protected GameObject[] objs = null;
    [SerializeField] protected int objNum = 0;
    [SerializeField] protected float heightRangeMin = 0.0f;
    [SerializeField] protected float heightRangeMax = 0.0f;
    [SerializeField] protected float bgDivided = 0.0f;

    protected bool widthOK = true;
    protected bool heightOK = true;

    public struct ObjSpaceRange
    {
        public float widthMin;
        public float widthMax;
        public float heightMin;
        public float heightMax;
    }

    [SerializeField] protected ObjSpaceRange objSpaceRange;

    protected virtual void Awake()
    {
        objs = new GameObject[objNum];

        bgDivided = ScreenInfo.bgFixedSizeMatchX.y / objs.Length;
    }

    public virtual GameObject[] CreateObjs(GameObject obj_previous_bg = null)
    {
        return null;
    }

    protected virtual Vector3 CreateRandomPosition(int height_idx, GameObject obj_previous_bg = null)
    {
        return Vector3.zero;
    }

    // オブジェクトとの横距離が近すぎるか、もしくは遠すぎるかをチェック
    protected bool CheckIfWidthSpaceOK(float pos_x_a, float pos_x_b, float range_min, float range_max)
    {
        if (Mathf.Abs(pos_x_a - pos_x_b) < range_min || Mathf.Abs(pos_x_a - pos_x_b) > range_max)
        {
            return false;
        }

        return true;
    }

    // オブジェクトとの縦距離が近すぎるかをチェック
    protected bool CheckIfHeightSpaceOK(float pos_y_a, float pos_y_b, float range_min)
    {
        if (Mathf.Abs(pos_y_a - pos_y_b) < range_min)
        {
            return false;
        }

        return true;
    }

    public ObjSpaceRange GetObjSpaceRange()
    {
        return objSpaceRange;
    }
}
