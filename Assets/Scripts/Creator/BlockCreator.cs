using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : ObjectCreator
{
    [SerializeField] private Rigidbody2D playerRb = null;
    [SerializeField] private float blockWidthSpaceMinDefault = 0.0f;
    [SerializeField] private float blockWidthSpaceMaxDefault = 0.0f;
    [SerializeField] private float blockHeightSpaceMinDefault = 0.0f;
    [SerializeField] private float blockHeightSpaceMaxDefault = 0.0f;

    protected override void Awake()
    {
        //objNum = (int)(objNum * Mathf.Sqrt(ScreenInfo.screenCoefficient.x));
        base.Awake();

        objSpaceRange.widthMin = blockWidthSpaceMinDefault * ScreenInfo.screenCoefficient.x;
        objSpaceRange.widthMax = blockWidthSpaceMaxDefault * ScreenInfo.screenCoefficient.x;

        objSpaceRange.heightMin = blockHeightSpaceMinDefault * ScreenInfo.screenCoefficient.x;
        objSpaceRange.heightMax = blockHeightSpaceMaxDefault * ScreenInfo.screenCoefficient.x;
    }

    public override GameObject[] CreateObjs(GameObject obj_previous_bg = null)
    {
        for (int i = 0; i < objs.Length; i++)
        {

            int blockType = Random.Range(0, 99) % objPrefabs.Length;

            objs[i] = Instantiate(objPrefabs[blockType], gameObject.transform);
            Vector3 position = CreateRandomPosition(i, obj_previous_bg);
            objs[i].transform.localPosition = position;

            BlockNormal block_script = objs[i].GetComponent<BlockNormal>();

            block_script.SetPlayerInfo(player, playerRb);
            block_script.SetTrajectory(trajectory);
        }

        return objs;
    }

    protected override Vector3 CreateRandomPosition(int height_idx, GameObject obj_previous_bg = null)
    {
        heightRangeMin = -ScreenInfo.bgFixedHalfSizeMatchX.y + height_idx * bgDivided;
        heightRangeMax = heightRangeMin + bgDivided / 2;

        widthOK = true;
        heightOK = true;

        Vector3 position = new Vector3(
            Random.Range(-ScreenInfo.bgHalfSizeMatchX.x, ScreenInfo.bgHalfSizeMatchX.x),
            Random.Range(heightRangeMin, heightRangeMax));

        objs[height_idx].transform.localPosition = position;

        if (height_idx > 0)
        {
            widthOK = CheckIfWidthSpaceOK(objs[height_idx - 1].transform.position.x, objs[height_idx].transform.position.x, objSpaceRange.widthMin, objSpaceRange.widthMax);
            heightOK = CheckIfHeightSpaceOK(objs[height_idx - 1].transform.position.y, objs[height_idx].transform.position.y, objSpaceRange.heightMin);
        }
        else if (height_idx == 0 && obj_previous_bg != null)
        {
            widthOK = CheckIfWidthSpaceOK(obj_previous_bg.transform.position.x, objs[height_idx].transform.position.x, objSpaceRange.widthMin, objSpaceRange.widthMax);
            heightOK = CheckIfHeightSpaceOK(obj_previous_bg.transform.position.y, objs[height_idx].transform.position.y, objSpaceRange.heightMin);
        }

        if (!widthOK || !heightOK)
        {
            position = CreateRandomPosition(height_idx, obj_previous_bg);
        }

        return position;
    }
}
