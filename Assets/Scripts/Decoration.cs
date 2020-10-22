using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
    public Sprite[] sprites;

    [SerializeField] private SpriteRenderer[] spriteRenderers;
    public float screenWidth;
    public float halfScreenWidth;
    public float screenHeight;
    public float halfScreenHeight;
    public int heightIndex;

    public GameObject decorationPrefab;
    public GameObject[] decorations;
    public DecorationMovement[] decorationMovement;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        screenWidth = (float)Screen.width / 100;
        halfScreenWidth = screenWidth / 2;
        screenHeight = (float)Screen.height / 100;
        halfScreenHeight = screenHeight / 2;
        heightIndex = (int)halfScreenHeight - 1;

        float fixedScreenHeight = screenHeight - 1.0f;
        float fixedHalfScreenHeight = fixedScreenHeight / 2;

        decorations = new GameObject[3];
        spriteRenderers = new SpriteRenderer[decorations.Length];
        decorationMovement = new DecorationMovement[decorations.Length];

        for (int i = 0; i < decorations.Length; i++)
        {
            Vector3 position = new Vector3(0.0f, -fixedHalfScreenHeight + (i + 1) * fixedScreenHeight / decorations.Length, 0.1f);
            decorations[i] = Instantiate(decorationPrefab, position, Quaternion.identity);
            decorations[i].transform.parent = transform;
            decorations[i].transform.localPosition = position;
            spriteRenderers[i] = decorations[i].GetComponent<SpriteRenderer>();
        }
    }

    public void ResetDocoration(int step_idx)
    {
        if (step_idx < 3)
        {
            SetSprites(0, 3);
            SetSpritesSize(0);
        }
        else
        {
            SetSprites(0, 3, 1);
            SetSpritesSize(1);
        }
    }

    private void SetSprites(int start_idx,int end_idx, int sprite_idx = 0)
    {
        for (int i = start_idx; i < end_idx; i++)
        {
            int idx = (Random.Range(0, 99) % 4 + 1) * sprite_idx;
            
            spriteRenderers[i].sprite = sprites[idx];

            spriteRenderers[i].transform.localPosition = new Vector3(
                Random.Range(-halfScreenWidth * Random.Range(0.8f, 1.2f), halfScreenWidth * Random.Range(0.8f, 1.2f)),
                spriteRenderers[i].transform.localPosition.y,
                spriteRenderers[i].transform.localPosition.z);
            spriteRenderers[i].color = new Vector4(spriteRenderers[i].color.r, spriteRenderers[i].color.g, spriteRenderers[i].color.b, 150.0f / 255);

            if (Random.Range(0, 99) % 2 == 0)
            {
                spriteRenderers[i].flipX = true;
            }
            else
            {
                spriteRenderers[i].flipX = false;
            }
        }
    }

    private void SetSpritesSize(int type)
    {
        if (type == 0)
        {
            for (int i = 0; i < decorations.Length; i++)
            {
                float width = Random.Range(0.6f, 1.2f);
                float height = Random.Range(0.3f, 0.6f);

                spriteRenderers[i].transform.localScale = new Vector3(width, height, 1.0f);
            }
        }
        else
        {
            for (int i = 0; i < decorations.Length; i++)
            {
                float size = Random.Range(0.3f, 0.5f);
                spriteRenderers[i].transform.localScale = new Vector3(size, size, 1.0f);
            }
        }
        
    }
}
