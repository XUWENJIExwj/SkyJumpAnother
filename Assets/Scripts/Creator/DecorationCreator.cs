using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationCreator : ObjectCreator
{
    [SerializeField] private float decorationHeightSpaceMinDefault = 0.0f;
    [SerializeField] private float decorationHeightSpaceMaxDefault = 0.0f;
    [SerializeField] private ObjectProperty[] objectPropertys = null;
    [SerializeField] private SpriteRenderer[] spriteRenderers = null;
    [SerializeField] private int step = 0;

    protected override void Awake()
    {
        base.Awake();

        objSpaceRange.heightMin = decorationHeightSpaceMinDefault * ScreenInfo.screenCoefficient.x;
        objSpaceRange.heightMax = decorationHeightSpaceMaxDefault * ScreenInfo.screenCoefficient.x;

        objectPropertys = new ObjectProperty[objNum];
        spriteRenderers = new SpriteRenderer[objNum];
    }

    public void SetStep(int step_idx)
    {
        step = step_idx;
    }

    public override GameObject[] CreateObjs(GameObject obj_previous_bg = null)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            int type = GetDecorationType();
            objs[i] = Instantiate(objPrefabs[type], gameObject.transform);
            Vector3 position = new Vector3(0.0f, -ScreenInfo.bgFixedHalfSizeMatchX.y + (i + 1) * bgDivided);
            objs[i].transform.localPosition = position;

            objectPropertys[i] = objs[i].GetComponent<ObjectProperty>();
            spriteRenderers[i] = objs[i].GetComponent<SpriteRenderer>();

            if (type == 0)
            {
                objectPropertys[i].SetSize(new Vector3(Random.Range(0.4f, 1.0f), Random.Range(0.1f, 0.4f), 1.0f));
                spriteRenderers[i].color = new Vector4(spriteRenderers[i].color.r, spriteRenderers[i].color.g, spriteRenderers[i].color.b, 150.0f / 255);
            }
            else
            {
                float size = Random.Range(0.18f, 0.25f);
                objectPropertys[i].SetSize(new Vector3(size, size, 1.0f));
            }

            if (Random.Range(0, 99) % 2 == 0)
            {
                spriteRenderers[i].flipX = true;
            }
            else
            {
                spriteRenderers[i].flipX = false;
            }

            objectPropertys[i] = null;
            spriteRenderers[i] = null;
        }

        return objs;
    }

    public int GetDecorationType()
    {
        if (step < 3)
        {
            return 0;
        }
        else
        {
            return Random.Range(0, 99) % 4 + 1;
        }
    }
}
