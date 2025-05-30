using UnityEngine;

public abstract class Character : MonoBehaviour
{
    #region Character Properties
    [Header("Character Properties")]
    public StatusBar healthBar;
    public float speedValue;
    #endregion

    #region Character Methods
    public abstract void Move();
    public abstract void DeathRoutine();
    public abstract void OnTriggerEnter(Collider other);
    public abstract void OnTriggerStay(Collider other);
    #endregion
}

