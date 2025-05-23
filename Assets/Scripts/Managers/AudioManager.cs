using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Properties
    // Singleton instance
    public static AudioManager manager;

    [Header("Audio Mixer and Clips")]
    public AudioMixer mixer;
    public AudioClip[] musics;
    public AudioClip[] sfx;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Volume Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    // Volume thresholds
    private const float MIN_VOLUME = -80f;
    private const float VOLUME_THRESHOLD = -20f;
    #endregion

    #region Initialization Routines
    void Awake()
    {
        // Implement Singleton pattern
        if (manager != null && manager != this)
        {
            Destroy(gameObject);
            return;
        }

        manager = this;
        DontDestroyOnLoad(gameObject);

        // Ensure music Source is assigned
        if (musicSource == null)
            musicSource = GetComponent<AudioSource>();

        // Ensure SFX Source exists
        if (sfxSource == null)
        {
            GameObject sfxObj = new GameObject("SFXSource");
            sfxObj.transform.SetParent(this.transform);
            sfxSource = sfxObj.AddComponent<AudioSource>();
        }
    }
    #endregion

    #region Switching Routines
    /// <summary>
    /// Plays a music track by index.
    /// </summary>
    public void SwitchMusic(int index)
    {
        if (index >= 0 && index < musics.Length)
        {
            musicSource.clip = musics[index];
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid music index.");
        }
    }

    /// <summary>
    /// Plays a sound effect by index.
    /// </summary>
    public void SwitchSFX(int index)
    {
        if (index >= 0 && index < sfx.Length)
        {
            sfxSource.clip = sfx[index];
            sfxSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid SFX index.");
        }
    }
    #endregion

    #region Volume Control
    /// <summary>
    /// Sets the master volume using a float value.
    /// </summary>
    public void ChangeMasterVolume(float volume)
    {
        mixer.SetFloat("MasterVol", volume > VOLUME_THRESHOLD ? volume : MIN_VOLUME);
    }

    /// <summary>
    /// Sets the music volume using a float value.
    /// </summary>
    public void ChangeMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVol", volume > VOLUME_THRESHOLD ? volume : MIN_VOLUME);
    }

    /// <summary>
    /// Sets the SFX volume using a float value.
    /// </summary>
    public void ChangeSFXVolume(float volume)
    {
        mixer.SetFloat("SFXVol", volume > VOLUME_THRESHOLD ? volume : MIN_VOLUME);
    }

    /// <summary>
    /// Loads current mixer values into sliders (called on startup or options menu).
    /// </summary>
    public void SetDefaultVolume()
    {
        if (mixer.GetFloat("MasterVol", out float masterVol) && masterSlider != null)
            masterSlider.value = masterVol;

        if (mixer.GetFloat("MusicVol", out float musicVol) && musicSlider != null)
            musicSlider.value = musicVol;

        if (mixer.GetFloat("SFXVol", out float sfxVol) && sfxSlider != null)
            sfxSlider.value = sfxVol;
    }

    /// <summary>
    /// Reads master volume from slider and applies it.
    /// </summary>
    public void ChangeMasterVolume()
    {
        if (masterSlider != null)
            ChangeMasterVolume(masterSlider.value);
    }

    /// <summary>
    /// Reads music volume from slider and applies it.
    /// </summary>
    public void ChangeMusicVolume()
    {
        if (musicSlider != null)
            ChangeMusicVolume(musicSlider.value);
    }

    /// <summary>
    /// Reads SFX volume from slider and applies it.
    /// </summary>
    public void ChangeSFXVolume()
    {
        if (sfxSlider != null)
            ChangeSFXVolume(sfxSlider.value);
    }
    #endregion
}
