using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsFXManager : MonoBehaviour
{
    public static SoundsFXManager instance;
    [SerializeField] private AudioSource soundFXObject; 

    private void Awake()
    {
       if (instance == null) 
       {
            instance = this;
       }
    }


    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
            AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

            audioSource.clip = audioClip;

            audioSource.volume = volume;

            audioSource.Play();

            float clipLength = audioSource.clip.length;

            Destroy(audioSource.gameObject, clipLength);
    }

}
