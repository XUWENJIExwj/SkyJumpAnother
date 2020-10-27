using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleWalk : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites = null;
    [SerializeField] private float framesPerSec = 3;
    [SerializeField] private Image image = null;

    private void FixedUpdate()
    {
        int index = (int)(Time.fixedTime * framesPerSec) % 2;
        image.sprite = sprites[index];
    }
}
