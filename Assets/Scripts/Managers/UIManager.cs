using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region Properties
    [Header("UI Elements")]
    public static UIManager Instance { get; private set; }
    private Dictionary<UINames, GameObject> uiDictionary = new Dictionary<UINames, GameObject>();
    public GameObject gameInterface;

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

    //Fix

    public void GameSceneLoad()
    {
        SceneManager.LoadScene(1); // Load the game scene
        HidePanel(UINames.MainMenu); // Hide the main menu panel
        Time.timeScale = 1f; // Ensure the game runs at normal speed
    }
}
