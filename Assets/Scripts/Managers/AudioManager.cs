using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Variaveis de audio
    public static AudioManager manager;
    public AudioMixer mixer;
    public AudioClip[] musics;
    public AudioClip[] sfx;
    public AudioSource musicsource;
    public AudioSource sfxsource;


    // Invoca o "manager", que passa para o UIController fazendo entao o controle do volume
    void Awake()
    {
        if (manager == null)
        {
            manager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        musicsource = this.GetComponent<AudioSource>();
    }

    // Muda as musicas para cada ocasiao (pause, play, derrota, etc.)
    public void SwitchMusic(int indice)
    {
        musicsource.clip = musics[indice];

        musicsource.Play();
    }

    public void SwitchSFX(int indice)
    {
        sfxsource.clip = sfx[indice];

        sfxsource.Play();
    }

    // Mudanca do volume max
    public void ChangeMasterVolume(float vol)
    {
        if (vol > -20)
        {
            mixer.SetFloat("MasterVol", vol);
        }
        else
        {
            mixer.SetFloat("MasterVol", -80);
        }
    }

    // Mudanca do volume da musica
    public void ChangeMusicVolume(float vol)
    {
        if (vol > -20)
        {
            mixer.SetFloat("MusicVol", vol);
        }
        else
        {
            mixer.SetFloat("MusicVol", -80);
        }
    }

    // Mudanca do volume dos efeitos sonoros
    public void ChangeSFXVolume(float vol)
    {
        if (vol > -20)
        {
            mixer.SetFloat("SFXVol", vol);
        }
        else
        {
            mixer.SetFloat("SFXVol", -80);
        }
    }
}