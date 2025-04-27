using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public UpgradeButton upgradeButton;
    public void UpdateShop(){
        if(upgradeButton != null){
            GameObject.FindAnyObjectByType<ShopMenuManager>().UpdateShop(upgradeButton);
        }
    }
}
