using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : ObjectCreator
{
    [SerializeField] private Camera cam = null;
    [SerializeField] protected float dotDistance = 0.0f;
    [SerializeField] protected TextMesh textMesh = null;
    [SerializeField] protected int lineIndex = 50;
    [SerializeField] protected Vector3 posDeviation = Vector3.zero;

    private Vector3 position;
    private float linePosYIndex;
    private int step = 0;

    protected override void Awake()
    {
        linePosYIndex = ScreenInfo.bgPosYDeviationMatchX * ScreenInfo.screenCoefficient.x - ScreenInfo.cameraOrthographicSize;
        transform.position = new Vector3(0.0f, 10.0f * ScreenInfo.screenCoefficient.x + linePosYIndex, 3.0f);
        SetText(100);

        objNum = (int)Mathf.Ceil(objNum * ScreenInfo.screenCoefficient.x);

        dotDistance = ScreenInfo.screenSize.x / (objNum - 1);

        objs = new GameObject[objNum];

        for (int i = 0; i < objNum; i++)
        {
            Vector3 pos = new Vector3(-ScreenInfo.screenHalfSize.x + dotDistance * i, 0.0f, 0.0f);
            objs[i] = Instantiate(objPrefabs[0], transform);
            objs[i].transform.localPosition = pos;
        }

        position = transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(
            position.x + (ScreenInfo.screenCoefficient.y / ScreenInfo.screenCoefficient.x - 1) * posDeviation.x,
            position.y + (ScreenInfo.screenCoefficient.y / ScreenInfo.screenCoefficient.x - 1) * posDeviation.y,
            position.z);
    }

    public void UpdateLine(float cam_pos_y)
    {
        if (transform.position.y < cam.transform.position.y-ScreenInfo.screenHalfSize.y)
        {
            step++;
            SetPosY();
            SetText(step * lineIndex * Score.scoreIndex);
        }
    }

    public void SetPosY()
    {
        transform.position = new Vector3(transform.position.x, step * lineIndex * ScreenInfo.screenCoefficient.x + linePosYIndex, transform.position.z);
        position = transform.position;
        FixPos();
    }

    public void SetText(int num)
    {
        textMesh.text = num.ToString() + "m";
    }

    private void FixPos()
    {
        transform.position = new Vector3(
            transform.position.x + (ScreenInfo.screenCoefficient.y / ScreenInfo.screenCoefficient.x - 1) * posDeviation.x,
            transform.position.y + (ScreenInfo.screenCoefficient.y / ScreenInfo.screenCoefficient.x - 1) * posDeviation.y,
            transform.position.z);
    }
}
