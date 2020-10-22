using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public int num;
    public float dotDistance;
    public GameObject dotPrefaba;
    public GameObject[] dots;
    public float screenWidth;
    public float screenHalfWidth;
    public TextMesh textMesh;

    // Start is called before the first frame update
    void Start()
    {
        screenWidth = (float)Screen.width / 100;
        screenHalfWidth = screenWidth / 2;
        dotDistance = screenWidth / (num - 1);

        dots = new GameObject[num];

        for (int i = 0; i < num; i++)
        {
            Vector3 pos = new Vector3(-screenHalfWidth + dotDistance * i, 0.0f, 0.0f);
            dots[i] = Instantiate(dotPrefaba, pos, Quaternion.identity, gameObject.transform);
            dots[i].transform.localPosition = new Vector3(dots[i].transform.localPosition.x, 0.0f, 0.0f);
        }
    }

    public void SetText(int num)
    {
        textMesh.text = num.ToString() + "m";
    }

    public void SetText(string str)
    {
        textMesh.text = str;
    }
}
