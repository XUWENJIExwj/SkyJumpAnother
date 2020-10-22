using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    [SerializeField] private Text text = null;
    private TouchScreenKeyboard keyboard = null;

    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor)
        {

        }
        else
        {
            if (keyboard != null)
            {
                if (keyboard.status == TouchScreenKeyboard.Status.Visible)
                {
                    text.text = keyboard.text;
                }
                if (keyboard.status == TouchScreenKeyboard.Status.Done)
                {
                    text.text = keyboard.text;
                    keyboard = null;
                }
            }
        }
    }

    public void ActiveKeyboard()
    {
        if (Application.isEditor)
        {
            
        }
        else
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        }
    }
}
