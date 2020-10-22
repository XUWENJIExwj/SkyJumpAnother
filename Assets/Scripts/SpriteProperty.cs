using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteProperty : MonoBehaviour
{
    public float width;
    public float height;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        spriteRenderer.size = new Vector2(width, height);
    }
}
