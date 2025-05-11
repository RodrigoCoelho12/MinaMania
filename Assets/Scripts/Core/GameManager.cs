using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Propriedades

    [Header("Paineis")]
    public GameObject main_menu_panel;
    public GameObject settings_panel;
    public GameObject store_panel;
    public GameObject insert_username_panel;
    public GameObject hud_panel;
    public GameObject pause_panel;
    public GameObject ranking_panel;
    public GameObject dialog_panel;
    public GameObject credit_panel;
    public GameObject specific_credit_panel;
    public GameObject game_over_panel;

    [Header("Utilitários")]
    public bool isPaused;

    #endregion

    #region Métodos

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    private void Update()
    {
        // Cheats();
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Pause();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) &&  isPaused)
        {
            UnPause(pause_panel);
        }
    }

    private void Cheats()
    {

    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
            
        pause_panel.SetActive(true);
    }
    public void UnPause(GameObject currentPanel)
    {
        Time.timeScale = 1f;
        isPaused = false;

        currentPanel.SetActive(false);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        isPaused = true;

        ShowGameOverPanel();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    private void ShowMainMenu()
    {
        main_menu_panel.SetActive(true);
        settings_panel.SetActive(false);
        store_panel.SetActive(false);
        insert_username_panel.SetActive(false);
        hud_panel.SetActive(false);
        pause_panel.SetActive(false);
        ranking_panel.SetActive(false);
        dialog_panel.SetActive(false);
        credit_panel.SetActive(false);
        specific_credit_panel.SetActive(false);
        game_over_panel.SetActive(false);
    }

    private void HideMainMenu()
    {
        main_menu_panel.SetActive(false);
    }

    private void ShowHUD()
    {
        hud_panel.SetActive(true);
    }

    private void ShowGameOverPanel()
    {
        game_over_panel.SetActive(true);
        hud_panel.SetActive(false);
    }

    public void ShowSettingsPanel()
    {
        settings_panel.SetActive(true);
        main_menu_panel.SetActive(false);
    }

    public void ShowStorePanel()
    {
        store_panel.SetActive(true);
        main_menu_panel.SetActive(false);
    }

    public void ShowInsertUsernamePanel()
    {
        insert_username_panel.SetActive(true);
        main_menu_panel.SetActive(false);
    }

    public void ShowRankingPanel()
    {
        ranking_panel.SetActive(true);
        main_menu_panel.SetActive(false);
    }

    public void ShowDialogPanel()
    {
        dialog_panel.SetActive(true);
        main_menu_panel.SetActive(false);
    }

    public void ShowCreditPanel()
    {
        credit_panel.SetActive(true);
        main_menu_panel.SetActive(false);
    }

    public void ShowSpecificCreditPanel()
    {
        specific_credit_panel.SetActive(true);
        credit_panel.SetActive(false);
    }

    public void ShowSpecificCreditBack()
    {
        specific_credit_panel.SetActive(false);
        credit_panel.SetActive(true);
    }

    #endregion
}
