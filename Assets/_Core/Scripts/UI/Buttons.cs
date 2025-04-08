using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class Buttons : MonoBehaviour
{
    //Classe dedicada à funcionalidades associadas aos botões

    #region Cenas
    void LoadMenu() => SceneManager.LoadScene((int)Scene.MainMenu);
    void LoadGame() => SceneManager.LoadScene((int)Scene.Game);
    void ExitGame() => Application.Quit();
    #endregion
    #region Menus
    //Shop
    public void UpdateShop(){
        UpgradeButton upgradebutton = this.GetComponent<UpgradeButton>();
        if(upgradebutton != null){
            GameObject.FindAnyObjectByType<ShopMenuManager>().UpdateShop(upgradebutton);
        }
    }
    #endregion
}
