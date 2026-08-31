using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SlidersAudio : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider sliderMaster, sliderAudio, sliderMusic;
     
     public void SettingSliders()
     {
         float valor = 0;
         
         mixer.GetFloat("masterVolume",out valor);
         sliderMaster.value = math.pow(10, valor / 20);
         
         mixer.GetFloat("musicVolume",out valor);
         sliderMusic.value = math.pow(10, valor / 20);
         
         mixer.GetFloat("soundVolume",out valor);
         sliderAudio.value = math.pow(10, valor / 20);
     }

     public void SliderMasterVolume()
     {
         mixer.SetFloat("masterVolume", math.log10(sliderMaster.value) * 20);
     }

    public void SliderMusicVolume()
    {
        mixer.SetFloat("musicVolume", math.log10(sliderMusic.value)*20);
    }

    public void SliderSoundVolume()
    {
        mixer.SetFloat("soundVolume", math.log10(sliderAudio.value)*20);
    }
}
