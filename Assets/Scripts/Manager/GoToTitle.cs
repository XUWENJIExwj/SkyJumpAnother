using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToTitle : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("Title");
    }
}
