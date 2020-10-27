using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerSoul : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSoul = null;
    [SerializeField] private float colorStartCoefficient = 0.25f;
    [SerializeField] private float colorEndCoefficient = 0.7f;
    [SerializeField] private Vector2 soulMoveEnd = Vector2.zero;
    [SerializeField] private Vector2 soulMoveYoyo = Vector2.zero;
    [SerializeField] private Vector3[] soulMovePath = null;
    [SerializeField] private float playTime = 2.5f;

    public void SetSoulColor()
    {
        playerSoul.color = GetFixedColor(colorStartCoefficient);
    }

    public Color GetFixedColor(float coefficient)
    {
        Color color= new Vector4(
            SkinInfo.skinColor.r * coefficient,
            SkinInfo.skinColor.g * coefficient,
            SkinInfo.skinColor.b * coefficient,
            SkinInfo.skinColor.a);

        return color;
    }

    public void PlayerSoulAnimation()
    {
        soulMoveEnd *= ScreenInfo.screenCoefficient.x;

        for (int i = 0; i < soulMovePath.Length; i++)
        {
            soulMovePath[i] *= ScreenInfo.screenCoefficient.x;
            soulMovePath[i].z = -3.0f;
        }

        transform.DOPath(soulMovePath, playTime, PathType.Linear, PathMode.Ignore);
        playerSoul.DOColor(GetFixedColor(colorEndCoefficient), playTime);
    }
}
