using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "width:" + Screen.width.ToString() + "    height:" + Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
