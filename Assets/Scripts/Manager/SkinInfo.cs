using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinInfo : MonoBehaviour
{
    public enum SkinType
    {
        SKIN_TYPE_00,
        SKIN_TYPE_01,
        SKIN_TYPE_02,
        SKIN_TYPE_03,
    }

    // 完成後削除
    bool isExist = false;

    static public SkinType skinType { get; private set; }
    static public Color skinColor { get; private set; }

    static public float colorCoefficient { get; private set; }

    private void Awake()
    {
        skinType = SkinType.SKIN_TYPE_00;
        skinColor = Color.white;
        colorCoefficient = 140 / 255.0f;

        // 完成後削除
        if (!isExist)
        {
            DontDestroyOnLoad(gameObject);
            isExist = true;
        }

        // 完成後復元
        //DontDestroyOnLoad(gameObject);
    }

    static public void SetSkinInfo(SkinType type, Color color)
    {
        skinType = type;
        skinColor = color;
    }
}
