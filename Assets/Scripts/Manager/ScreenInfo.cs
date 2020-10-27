using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenInfo : MonoBehaviour
{
    [SerializeField] private Vector2 ScreenDefaultSize = Vector2.zero;
    [SerializeField] private Vector2 BgDefaultSize = Vector2.zero;
    [SerializeField] private float BgHeightSpace = 0.0f;
    [SerializeField] [Range(0.0f, 1.0f)] private float bgHeightSpaceCoefficient = 0.0f;

    static public Vector2 screenDefaultSize { get; private set; }
    static public Vector2 screenSize { get; private set; }
    static public Vector2 screenHalfSize { get; private set; }
    static public Vector2 screenCoefficient { get; private set; } // 拡大用の係数
    static public Vector2 bgDefaultSize { get; private set; }
    static public Vector2 bgSizeMatchX { get; private set; } // Widthによる拡大縮小
    static public Vector2 bgHalfSizeMatchX { get; private set; }
    static public float bgPosYDeviationMatchX { get; private set; }
    static public Vector2 bgSizeMatchY { get; private set; } // Heightによる拡大縮小
    static public Vector2 bgHalfSizeMatchY { get; private set; }
    static public float bgHeightSpace { get; private set; }
    static public Vector2 bgFixedSizeMatchX { get; private set; }
    static public Vector2 bgFixedHalfSizeMatchX { get; private set; }

    static public float cameraOrthographicSize { get; private set; }

    private void Awake()
    {
        // ScreenInfoの初期化
        screenDefaultSize = ScreenDefaultSize;
        screenSize = new Vector2((float)Screen.width / 100, (float)Screen.height / 100);
        screenHalfSize = screenSize / 2;
        screenCoefficient = screenSize / BgDefaultSize;

        bgDefaultSize = BgDefaultSize;
        bgSizeMatchX = new Vector2(screenSize.x, BgDefaultSize.y * screenCoefficient.x);
        bgHalfSizeMatchX = bgSizeMatchX / 2;
        bgPosYDeviationMatchX = screenHalfSize.y - bgHalfSizeMatchX.y;

        bgSizeMatchY = new Vector2(BgDefaultSize.x * screenCoefficient.y, screenSize.y);
        bgHalfSizeMatchY = bgSizeMatchY / 2;

        BgHeightSpace = bgSizeMatchX.y * bgHeightSpaceCoefficient;
        bgHeightSpace = BgHeightSpace;

        bgFixedSizeMatchX = new Vector2(bgSizeMatchX.x, bgSizeMatchX.y - bgHeightSpace);
        bgFixedHalfSizeMatchX = bgFixedSizeMatchX / 2;

        cameraOrthographicSize = Mathf.Max(screenHalfSize.x, screenHalfSize.y);

        DontDestroyOnLoad(gameObject);
    }
}
