using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinInfo : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 255.0f)] private float ColorCoefficient = 0.0f;
    public enum SkinType
    {
        SKIN_TYPE_00,
        SKIN_TYPE_01,
        SKIN_TYPE_02,
        SKIN_TYPE_03,
    }

    static public SkinType skinType { get; private set; }
    static public Color skinColor { get; private set; }

    static public Color skinLitColor { get; private set; }

    static public float colorCoefficient { get; private set; }

    private void Awake()
    {
        skinType = SkinType.SKIN_TYPE_00;
        skinColor = Color.white;
        colorCoefficient = ColorCoefficient / 255.0f;
        SetSkinLitColor();

        DontDestroyOnLoad(gameObject);
    }

    static public void SetSkinInfo(SkinType type, Color color)
    {
        skinType = type;
        skinColor = color;
        SetSkinLitColor();
    }

    static public void SetSkinLitColor()
    {
        skinLitColor = new Vector4(skinColor.r * colorCoefficient, skinColor.g * colorCoefficient, skinColor.b * colorCoefficient, skinColor.a);
    }
}
