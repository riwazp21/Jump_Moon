using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class soundMixer : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("masterVolume",Mathf.Log(level) * 20f);
    }
    public void SetSoundFX(float level)
    {
        audioMixer.SetFloat("soundFXVolume",Mathf.Log(level) * 20f);
    }
    public void SetMusic(float level)
    {
        audioMixer.SetFloat("musicVolume",Mathf.Log(level) * 20f);
    }
}
