using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Properties
    [Header("UI Elements")]
    public static UIManager Instance { get; private set; }
    private Dictionary<UINames, GameObject> uiDictionary = new Dictionary<UINames, GameObject>();
    public GameObject gameInterface;
    [SerializeField] GameObject mainMenuFirstButton;
    [SerializeField] GameObject rankingFirstButton;
    [SerializeField] GameObject settingsFirstButton;
    [SerializeField] GameObject graphicsFirstButton;
    [SerializeField] GameObject audioFirstButton;
    [SerializeField] GameObject gamepadControls;
    [SerializeField] GameObject keyboardControls;
    [SerializeField] GameObject gamepadControlsFirstButton;
    [SerializeField] GameObject keyboardControlsFirstButton;

    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    #endregion

    #region Initialization Routines
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (gameInterface == null)
        {
            Debug.LogWarning("[UIManager] Interface GameObject is not assigned in the Inspector.");
            try
            {
                gameInterface = GameObject.Find("GameInterface");
                if (gameInterface == null)
                {
                    Debug.LogError("[UIManager] GameInterface GameObject not found in the scene.");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[UIManager] Error finding GameInterface: {e.Message}");
            }
        }


        //DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(gameInterface);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.instance.SwitchMusic(0);
        SetDefaultVolume();

        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
        InitializeUIDictionary();
    }

    //Fill a private Dictionary that relates the enum UINames with the GameObject of the panel
    //The panel name is the enum name + " Panel", obtaineted by the extension of UINames
    private void InitializeUIDictionary()
    {
        foreach (UINames name in Enum.GetValues(typeof(UINames)))
        {
            string panelName = name.GetPanelName();
            GameObject panel = GameObject.Find(panelName);

            if (panel != null)
            {
                Debug.Log($"[UIManager] Panel '{panelName}' found and registered.");
                uiDictionary[name] = panel;
                panel.SetActive(false); //Deactivates the panel by default
            }
            else
            {
                Debug.LogWarning($"[UIManager] Panel '{panelName}' not found on scene.");
            }
        }
        ShowPanel(UINames.MainMenu); // Show the main menu by default
    }
    #endregion

    #region Auxiliar Methods
    public void ShowPanel(UINames name)
    {
        if (uiDictionary.TryGetValue(name, out GameObject panel))
        {
            panel.SetActive(true);
        }
        else
        {
            Debug.LogError($"[UIManager] Panel '{name.GetPanelName()}' not registered.");
        }
    }

    public void HidePanel(UINames name)
    {
        if (uiDictionary.TryGetValue(name, out GameObject panel))
        {
            panel.SetActive(false);
        }
        else
        {
            Debug.LogError($"[UIManager] Painel '{name.GetPanelName()}' não está registrado.");
        }
    }

    public GameObject GetPanel(UINames name)
    {
        uiDictionary.TryGetValue(name, out GameObject panel);
        return panel;
    }
    #endregion

    public void GameSceneLoad()
    {
        ShowPanel(UINames.Loading);
        SceneManager.LoadScene(1); // Load the game scene
        HidePanel(UINames.MainMenu); // Hide the main menu panel
        Time.timeScale = 1f; // Ensure the game runs at normal speed
    }

    private void Update()
    {
        if(UserInputManager.instance.isUsingGamepad)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void SetSelectedPanelButton(GameObject panel)
    {
        if(panel.name == "Ranking Panel")
        {
            EventSystem.current.SetSelectedGameObject(rankingFirstButton);
        }
        else if(panel.name == "Settings Panel")
        {
            EventSystem.current.SetSelectedGameObject(settingsFirstButton);
        }
        else if(panel.name == "GraphicsSettings Panel")
        {
            EventSystem.current.SetSelectedGameObject(graphicsFirstButton);
        }
        else if (panel.name == "AudioSettings Panel")
        {
            EventSystem.current.SetSelectedGameObject(audioFirstButton);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
        }

    }

    public void ShowCorrectControls()
    {
        if (UserInputManager.instance.isUsingGamepad)
        {
            gamepadControls.SetActive(true); 
            EventSystem.current.SetSelectedGameObject(gamepadControlsFirstButton);
        }
        else
        {
            keyboardControls.SetActive(true);
            EventSystem.current.SetSelectedGameObject(keyboardControlsFirstButton);
        }
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1f;
    }
    
    // Mudar o master (volume todo)
    public void ChangeMasterVolume()
    {
        AudioManager.instance.ChangeMasterVolume(masterSlider.value);
    }

    // Mudar o volume da musica (so muda a musica)
    public void ChangeMusicVolume()
    {
        AudioManager.instance.ChangeMusicVolume(musicSlider.value);
    }

    // Mudar o volume do SFX (so muda os efeitos sonoros)
    public void ChangeSFXVolume()
    {
        AudioManager.instance.ChangeSFXVolume(sfxSlider.value);
    }
    
    public void SetDefaultVolume()
    {
        AudioManager.instance.mixer.GetFloat("MasterVol", out float aux1);

        if (masterSlider != null)
        {
            masterSlider.value = aux1;
        }

        AudioManager.instance.mixer.GetFloat("MusicVol", out float aux2);

        if (musicSlider != null)
        {
            musicSlider.value = aux2;
        }

        AudioManager.instance.mixer.GetFloat("SFXVol", out float aux3);

        if (sfxSlider != null)
        {
            sfxSlider.value = aux3;
        }
    }
}
