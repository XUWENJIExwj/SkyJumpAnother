﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenInfo : MonoBehaviour
{
    [SerializeField] private Vector2 screenDefaultSize = Vector2.zero;
    [SerializeField] private Vector2 bgDefaultSize = Vector2.zero;

    static public Vector2 ScreenOriginSize { get; private set; }
    static public Vector2 screenSize { get; private set; }
    static public Vector2 screenHalfSize { get; private set; }
    static public Vector2 screenCoefficient { get; private set; } // 拡大用の係数
    static public Vector2 bgSizeXC { get; private set; } // Widthによる拡大縮小
    static public Vector2 bgSizeYC { get; private set; } // Heightによる拡大縮小
    static public float cameraOrthographicSize { get; private set; }

    // 完成後削除
    bool isExist = false;

    // Start is called before the first frame update
    void Awake()
    {
        // ScreenInfoの初期化
        ScreenOriginSize = screenDefaultSize;
        screenSize = new Vector2((float)Screen.width / 100, (float)Screen.height / 100);
        screenHalfSize = screenSize / 2;
        screenCoefficient = screenSize / bgDefaultSize;

        bgSizeXC = new Vector2(screenSize.x, bgDefaultSize.y * screenCoefficient.x);
        bgSizeYC = new Vector2(bgDefaultSize.x * screenCoefficient.y, screenSize.y);

        // 完成後削除
        if (!isExist)
        {
            DontDestroyOnLoad(gameObject);
            isExist = true;
        }

        cameraOrthographicSize = Mathf.Max(screenSize.x, screenSize.y);

        //Debug.Log("Width: " + Screen.width);
        //Debug.Log("Height: " + Screen.height);

        // 完成後復元
        //DontDestroyOnLoad(gameObject);
        //SceneManager.LoadScene("Title");
    }
}
