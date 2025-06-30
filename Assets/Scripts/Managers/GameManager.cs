using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public Slider masterslider, musicslider, sfxslider;

    private GameObject currentPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject pausePanelFirst;
    [SerializeField] GameObject settingsPanelFirst;
    [SerializeField] GameObject keyboardControlsPanelFirst;
    [SerializeField] GameObject gamepadControlsPanelFirst;
    [SerializeField] GameObject inputUserNamePanelFirst;
    [SerializeField] GameObject audioSettingsPanelFirst;
    [SerializeField] GameObject graphicsSettingsPanelFirst;
    [SerializeField] GameObject inputUserNamePanel;
    [SerializeField] Player player;

    public bool isPaused;

    void Start()
    {
        AudioManager.instance.SwitchMusic(1);
        SetDefaultVolume();
        
        //Time.timeScale = 1f;
        //isPaused = true;
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

    // Mud ar o master (volume todo)
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
        player.enabled = false;

        isPaused = true;
    }
    public void UnpauseGame()
    {
        currentPanel.SetActive(false);

        if (currentPanel.name == "GraphicsSettings Panel" ||  currentPanel.name == "AudioSettings Panel")
        {
            currentPanel = settingsPanel;
        }
        else if (currentPanel != pausePanel && currentPanel != null)
        {
            currentPanel = pausePanel;
        }
        else
        {
            Time.timeScale = 1f;
            player.enabled = true;
            isPaused = false;
        }
    }

    public void SetCurrentPanel(GameObject panel)
    {
        currentPanel = panel;

        if (currentPanel.name == "Pause Panel") 
        { 
            EventSystem.current.SetSelectedGameObject(pausePanelFirst);
        } 
        else if(currentPanel.name == "Settings Panel")
        {
            EventSystem.current.SetSelectedGameObject(settingsPanelFirst);
        }
        else if (currentPanel.name == "KeyBind_Keyboard Panel")
        {
            EventSystem.current.SetSelectedGameObject(keyboardControlsPanelFirst);
        }
        else if (currentPanel.name == "KeyBind_Gamepad Panel")
        {
            EventSystem.current.SetSelectedGameObject(gamepadControlsPanelFirst);
        }
        else if (currentPanel.name == "InputUsername Panel")
        {
            EventSystem.current.SetSelectedGameObject(inputUserNamePanelFirst);
        }
        else if(currentPanel.name == "AudioSettings Panel")
        {
            EventSystem.current.SetSelectedGameObject(audioSettingsPanelFirst);
        }
        else if (currentPanel.name == "GraphicsSettings Panel")
        {
            EventSystem.current.SetSelectedGameObject(graphicsSettingsPanelFirst);
        }
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        inputUserNamePanel.SetActive(true);
        SetCurrentPanel(inputUserNamePanel);

        isPaused = true;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
