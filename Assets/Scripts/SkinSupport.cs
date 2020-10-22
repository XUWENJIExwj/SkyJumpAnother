using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSupport : MonoBehaviour
{
    [SerializeField] private bool isExist = false;
    [SerializeField] private int playerType = 0;
    [SerializeField] private Color playerColor = Color.white;

    private void Start()
    {
        if (!isExist)
        {
            DontDestroyOnLoad(gameObject);
            isExist = true;
        }
    }

    public void SetPlayerColorInfo(int type, Color color)
    {
        playerType = type;
        playerColor = color;
    }

    public int GetPlayerType()
    {
        return playerType;
    }

    public Color GetPlayerColor()
    {
        return playerColor;
    }
}
