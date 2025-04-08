using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
class ShopMenuManager : MonoBehaviour
{
    public TMP_Text name;
    public TMP_Text description;
    public TMP_Text price;
    public void UpdateShop(UpgradeButton current){
        name.text = current.name;
        description.text = current.description;
        price.text = current.price.ToString();
    }
}