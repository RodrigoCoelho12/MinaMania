using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public int priceValue {  get; private set; }

    public void PrintPriceValue()
    {
        Debug.Log("Price: " +priceValue);
    }
}
