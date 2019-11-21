using UnityEngine;
using System.Collections;

public class SoundEffectsManager : MonoBehaviour
{
    public AudioClip[] soundEffects;
    AudioSource source;
    public int soundInterval;
    float timer;
    void Start()
    {
       source =  GetComponent<AudioSource>();
       source.clip = soundEffects[Random.Range(0, soundEffects.Length)];
       source.Play();
    }

    void Update()
    {
        if(!source.isPlaying && (int)Time.time % soundInterval == 0 && (int)Time.time != 0)
        {
            source.clip = soundEffects[Random.Range(0, soundEffects.Length)];
            source.Play();
        }
    }
}
