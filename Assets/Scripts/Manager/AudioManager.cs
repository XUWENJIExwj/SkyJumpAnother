using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum AudioSourceIndex
    {
        AUDIO_SOURCE_BGM,
        AUDIO_SOURCE_SE_TYPE_A,
        AUDIO_SOURCE_SE_TYPE_B,
    }

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
        SE_NONE
    }

    [SerializeField] private int audioSourceNum = 0;
    [SerializeField] private AudioClip[] Bgm = null;
    [SerializeField] private AudioClip[] Se = null;

    static private AudioSource[] audioSource;
    static private AudioClip[] bgm;
    static private AudioClip[] se;

    private void Awake()
    {
        audioSource = new AudioSource[audioSourceNum];

        for (int i = 0; i < audioSourceNum; i++)
        {
            audioSource[i] = gameObject.AddComponent<AudioSource>();
        }

        bgm = Bgm;
        se = Se;

        DontDestroyOnLoad(gameObject);
    }

    static public void PlayBGM(BGM bgm_idx)
    {
        audioSource[(int)AudioSourceIndex.AUDIO_SOURCE_BGM].clip = bgm[(int)bgm_idx];
        audioSource[(int)AudioSourceIndex.AUDIO_SOURCE_BGM].Play();
        audioSource[(int)AudioSourceIndex.AUDIO_SOURCE_BGM].loop = true;
    }

    static public void StopBGM()
    {
        audioSource[(int)AudioSourceIndex.AUDIO_SOURCE_BGM].Stop();
    }

    static public void PlaySE(AudioSourceIndex idx, SE se_idx, float volume = 1.0f)
    {
        audioSource[(int)idx].PlayOneShot(se[(int)se_idx]);
        SetVolume(idx, volume);
    }

    static public void SetVolume(AudioSourceIndex idx, float volume = 1.0f)
    {
        audioSource[(int)idx].volume = volume;
    }

    static public bool GetIsPlaying(AudioSourceIndex idx)
    {
        return audioSource[(int)idx].isPlaying;
    }
}
