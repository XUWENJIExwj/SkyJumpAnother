using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : ObjectCreator
{
    [SerializeField] private Rigidbody2D playerRb = null;
    [SerializeField] private Score score = null;
    [SerializeField] private float blockWidthSpaceMin = 0.0f;
    [SerializeField] private float blockWidthSpaceMax = 0.0f;
    [SerializeField] private float blockWidthSpaceMinDefault = 0.0f;
    [SerializeField] private float blockWidthSpaceMaxDefault = 0.0f;
    [SerializeField] private float blockHeightSpaceMin = 0.0f;
    [SerializeField] private float blockHeightSpaceMax = 0.0f;
    [SerializeField] private float blockHeightSpaceMinDefault = 0.0f;
    [SerializeField] private float blockHeightSpaceMaxDefault = 0.0f;

    protected override void Awake()
    {
        base.Awake();

        blockWidthSpaceMin = blockWidthSpaceMinDefault * ScreenInfo.screenCoefficient.x;
        blockWidthSpaceMax = blockWidthSpaceMaxDefault * ScreenInfo.screenCoefficient.x;

        blockHeightSpaceMin = blockHeightSpaceMinDefault * ScreenInfo.screenCoefficient.x;
        blockHeightSpaceMax = blockHeightSpaceMaxDefault * ScreenInfo.screenCoefficient.x;
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
            block_script.player = player;
            block_script.trajectory = trajectory;
            block_script.playerRb = playerRb;
            block_script.score = score;
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
            widthOK = CheckIfWidthSpaceOK(objs[height_idx - 1].transform.position.x, objs[height_idx].transform.position.x, blockWidthSpaceMin, blockWidthSpaceMax);
            heightOK = CheckIfHeightSpaceOK(objs[height_idx - 1].transform.position.y, objs[height_idx].transform.position.y, blockHeightSpaceMin);
        }
        else if (height_idx == 0 && obj_previous_bg != null)
        {
            widthOK = CheckIfWidthSpaceOK(obj_previous_bg.transform.position.x, objs[height_idx].transform.position.x, blockWidthSpaceMin, blockWidthSpaceMax);
            heightOK = CheckIfHeightSpaceOK(obj_previous_bg.transform.position.y, objs[height_idx].transform.position.y, blockHeightSpaceMin);
        }

        if (!widthOK || !heightOK)
        {
            position = CreateRandomPosition(height_idx, obj_previous_bg);
        }

        return position;
    }
}
