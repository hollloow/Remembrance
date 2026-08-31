using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameManager instance;
    
    public static float TimerDoJogo;
    [SerializeField] AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void FixedUpdate() => TimerDoJogo = +Time.unscaledTime;

    public void Freze(float freezeAmount) => StartCoroutine(FreezeTime(freezeAmount));
    IEnumerator FreezeTime(float freezeAmount)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(freezeAmount);
        Time.timeScale = 1;
    }

    public void AudioManager(AudioClip clip, Transform spawnTransform, float volume)
    {
        //spawna o obj
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        
        //pega o clip
        audioSource.clip = clip;
        
        //pega o volume
        audioSource.volume = volume;
        
        //toca o audio
        audioSource.Play();
        
        //pega o tamanho do audio
        float clipLength = audioSource.clip.length;
        
        //destroi o obj dpois de terminar de tocar
        Destroy(audioSource.gameObject, clipLength);
    }
}
