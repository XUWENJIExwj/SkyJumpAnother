using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleWalk : MonoBehaviour
{
    public Sprite[] sprites;
    public float framesPerSec;

    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int index = (int)(Time.fixedTime * framesPerSec) % 2;
        image.sprite = sprites[index];
    }
}
