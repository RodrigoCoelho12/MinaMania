using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider masterslider, musicslider, sfxslider;

    private GameObject currentPanel;
    [SerializeField] GameObject pausePanel;

    public bool isPaused;

    void Start()
    {
        AudioManager.instance.SwitchMusic(0);
        SetDefaultVolume();
    }

    private void Update()
    {
        if (UserInputManager.instance.MenuOpenCloseInput && !isPaused)
        {
            PauseGame();
        }
        else if (UserInputManager.instance.MenuOpenCloseInput && isPaused)
        {
            UnpauseGame(); 
        }
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

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        SetCurrentPanel(pausePanel);

        isPaused = true;
    }
    public void UnpauseGame()
    {
        currentPanel.SetActive(false);
        if(currentPanel != pausePanel)
        {
            currentPanel = pausePanel;
        }
        else
        {
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    public void SetCurrentPanel(GameObject panel)
    {
        currentPanel = panel;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
