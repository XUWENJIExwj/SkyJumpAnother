using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckStringByte : MonoBehaviour
{
    [SerializeField] InputField inputField = null;
    [SerializeField] int byteMax = 0;

    private void Update()
    {
        CheckCharacterLimit();
    }

    public void CheckCharacterLimit()
    {
        inputField.characterLimit = byteMax;

        for (int i = 0; i < inputField.textComponent.text.Length; i++)
        {
            if(IfFullWidthCharacter(inputField.textComponent.text[i]))
            {
                inputField.characterLimit--;
            }

            if (inputField.characterLimit < 3)
            {
                inputField.characterLimit = 3;
                break;
            }
        }
    }

    public void MakeCharacterToLimit(string str)
    {
        int byteCnt = 0;

        for (int i = 0; i < str.Length; i++)
        {
            if (byteCnt < byteMax)
            {
                if (IfFullWidthCharacter(str[i]))
                {
                    byteCnt++;
                }

                byteCnt++;
            }
        }

        for (int i = 0; i < byteMax - byteCnt; i++)
        {
            str += " ";
        }
    }

    public bool IfFullWidthCharacter(char c)
    {
        return !((c >= 0x0 && c < 0x81) || (c == 0xf8f0) || (c >= 0xff61 && c < 0xffa0) || (c >= 0xf8f1 && c < 0xf8f4));
    }

    public bool IfHalfWidthSpace(char c)
    {
        return !IfFullWidthCharacter(c) && (c == 0x20);
    }
}