using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float damageValue { get; private set; }


    public abstract void Attack();
}
