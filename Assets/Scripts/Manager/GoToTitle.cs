using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToTitle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bg = null;
    private void Start()
    {
        SceneManager.LoadScene("Title");

        bg.size = ScreenInfo.screenSize;
    }
}
