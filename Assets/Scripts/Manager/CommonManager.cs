using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommonManager : MonoBehaviour
{
    [SerializeField] protected Camera cam = null;
    [SerializeField] protected UIProperty[] uiPartsXC = null; // Widthによる拡大縮小
    [SerializeField] protected UIProperty[] uiPartsYC = null; // Heightによる拡大縮小

    private void Awake()
    {
        cam.orthographicSize = ScreenInfo.cameraOrthographicSize;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }

    public void GoToNextScene(string next_scene)
    {
        SceneManager.LoadScene(next_scene);
        //if (fade.GetFadeState() == Fade.FadeState.FADE_STATE_NONE)
        //{
        //    
        //    fade.SetFadeState(Fade.FadeState.FADE_STATE_OUT);
        //    audioManager.PlaySE(AudioManager.SE.SE_TITLE, 1, 0.5f);
        //}
    }
}
