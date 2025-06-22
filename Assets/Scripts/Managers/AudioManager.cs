using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Variaveis de audio
    public static AudioManager instance;
    public AudioMixer mixer;
    public AudioClip[] musics;
    public AudioClip[] sfx;
    public List<AudioSource> enemySfx = new List<AudioSource>();
    public AudioSource musicSource;
    public AudioSource sfxSource;

    // Invoca o "manager", que passa para o UIController fazendo entao o controle do volume
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Muda as musicas para cada ocasiao (pause, play, derrota, etc.)
    public void SwitchMusic(int indice)
    {
        musicSource.clip = musics[indice];

        musicSource.Play();
    }

    public void SwitchSFX(int indice)
    {
        sfxSource.clip = sfx[indice];

        sfxSource.Play();
    }

    public void SwitchEnemySFX(int indice)
    {
        if (enemySfx.Count == 0) return;

        /*if (indice == 0)
        {
            enemySfx[indice].pitch = Random.Range(0.2f, 1.8f);
        }
        else
        {
            enemySfx[indice].pitch = 1;
        }*/

        enemySfx[indice].Play();
    }

    // Mudanca do volume max
    public void ChangeMasterVolume(float vol)
    {
        //if (vol > -20)
        //{
            mixer.SetFloat("MasterVol", vol);
        //}
        //else
        //{
        //    mixer.SetFloat("MasterVol", -80);
        //}
    }

    // Mudanca do volume da musica
    public void ChangeMusicVolume(float vol)
    {
        //if (vol > -20)
        //{
            mixer.SetFloat("MusicVol", vol);
        //}
        //else
        //{
        //    mixer.SetFloat("MusicVol", -80);
        //}
    }

    // Mudanca do volume dos efeitos sonoros
    public void ChangeSFXVolume(float vol)
    {
        //if (vol > -20)
        //{
            mixer.SetFloat("SFXVol", vol);
        //}
        //else
        //{
        //    mixer.SetFloat("SFXVol", -80);
        //}
    }
}