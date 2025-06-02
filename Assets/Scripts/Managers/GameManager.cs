using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider masterslider, musicslider, sfxslider;
    void Start()
    {
        AudioManager.instance.SwitchMusic(0);
    }
    public void SetDefaultVolume()
    {
        AudioManager.instance.mixer.GetFloat("MasterVol", out float aux1);

        if (masterslider != null)
        {
            masterslider.value = aux1;
        }

        AudioManager.instance.mixer.GetFloat("MusicVol", out float aux2);

        if (musicslider != null)
        {
            musicslider.value = aux2;
        }

        AudioManager.instance.mixer.GetFloat("SFXVol", out float aux3);

        if (sfxslider != null)
        {
            sfxslider.value = aux3;
        }
    }

    // Mudar o master (volume todo)
    public void ChangeMasterVolume()
    {
        AudioManager.instance.ChangeMasterVolume(masterslider.value);
    }

    // Mudar o volume da musica (so muda a musica)
    public void ChangeMusicVolume()
    {
        AudioManager.instance.ChangeMusicVolume(musicslider.value);
    }

    // Mudar o volume do SFX (so muda os efeitos sonoros)
    public void ChangeSFXVolume()
    {
        AudioManager.instance.ChangeSFXVolume(sfxslider.value);
    }
}
