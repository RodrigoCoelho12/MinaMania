using UnityEngine;
using TMPro;
class ShopMenuManager : MonoBehaviour
{
    public TMP_Text _name;
    public TMP_Text description;
    public TMP_Text price;
    public void UpdateShop(UpgradeButton current){
        _name.text = current.name;
        description.text = current.description;
        price.text = current.price.ToString();
    }
}