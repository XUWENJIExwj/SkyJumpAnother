using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum BGM
    {
        BGM_TITLE,
        BGM_GAME,
        BGM_RESULT,
    }

    public enum SE
    {
        SE_TITLE,
        SE_GAME_OVER,
        SE_GAME_PLAYER_JUMP,
        SE_GAME_PLAYER_COLLISION,
        SE_RESULT,
        SE_NONE
    }

    public AudioClip[] bgm;
    public AudioClip[] se;
    public int audioSourceNum;
    private AudioSource[] audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = new AudioSource[audioSourceNum];

        for (int i = 0; i < audioSourceNum; i++)
        {
            audioSource[i] = gameObject.AddComponent<AudioSource>();
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayBGM(BGM bgm_idx, int idx = 0)
    {
        audioSource[idx].clip = bgm[(int)bgm_idx];
        audioSource[idx].Play();
        audioSource[idx].loop = true;
    }

    public void PlaySE(SE se_idx, int idx, float volume = 1.0f)
    {
        audioSource[idx].PlayOneShot(se[(int)se_idx]);
        SetVolume(idx, volume);
    }

    public void SetVolume(int idx, float volume = 1.0f)
    {
        audioSource[idx].volume = volume;
    }

    public bool GetIsPlaying(int idx)
    {
        return audioSource[idx].isPlaying;
    }
}
