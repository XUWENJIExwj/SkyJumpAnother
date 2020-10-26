using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : ObjectCreator
{
    [SerializeField] private float enemyHeightSpaceMinDefault = 0.0f;
    [SerializeField] private float enemyHeightSpaceMaxDefault = 0.0f;

    public enum EnemyType
    {
        TYPE_BIRD,
        TYPE_UFO,
        TYPE_NONE
    }

    [SerializeField] private EnemyType enemyType = EnemyType.TYPE_NONE;
    [SerializeField] private int enemyEncount = 20;

    protected override void Awake()
    {
        base.Awake();

        objSpaceRange.heightMin = enemyHeightSpaceMinDefault * ScreenInfo.screenCoefficient.x;
        objSpaceRange.heightMax = enemyHeightSpaceMaxDefault * ScreenInfo.screenCoefficient.x;
    }

    public void SetEnemyType(EnemyType enemy_type)
    {
        enemyType = enemy_type;
    }

    public override GameObject[] CreateObjs(GameObject obj_previous_bg = null)
    {
        int encount = enemyEncount;

        for (int i = 0; i < objs.Length; i++)
        {
            objs[i] = Instantiate(objPrefabs[(int)enemyType], gameObject.transform);
            Vector3 position = CreateRandomPosition(i, obj_previous_bg);
            objs[i].transform.localPosition = position;

            if (encount < Random.Range(0, 99))
            {
                objs[i].SetActive(false);
                encount += enemyEncount;
            }
        }

        return objs;
    }

    protected override Vector3 CreateRandomPosition(int height_idx, GameObject obj_previous_bg = null)
    {
        heightRangeMin = -ScreenInfo.bgFixedHalfSizeMatchX.y + height_idx * bgDivided;
        heightRangeMax = heightRangeMin + bgDivided / 2;

        heightOK = true;

        Vector3 position = new Vector3(
            Random.Range(-ScreenInfo.bgHalfSizeMatchX.x, ScreenInfo.bgHalfSizeMatchX.x),
            Random.Range(heightRangeMin, heightRangeMax));

        objs[height_idx].transform.localPosition = position;

        if (height_idx > 0)
        {
            heightOK = CheckIfHeightSpaceOK(objs[height_idx - 1].transform.position.y, objs[height_idx].transform.position.y, objSpaceRange.heightMin);
        }
        else if (height_idx == 0 && obj_previous_bg != null)
        {
            heightOK = CheckIfHeightSpaceOK(obj_previous_bg.transform.position.y, objs[height_idx].transform.position.y, objSpaceRange.heightMin);
        }

        if (!heightOK)
        {
            position = CreateRandomPosition(height_idx, obj_previous_bg);
        }

        return position;
    }
}
