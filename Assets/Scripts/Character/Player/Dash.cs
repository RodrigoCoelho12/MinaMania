using UnityEngine;

public partial class Player : Character
{
    [Header("Dash Properties")]
    public bool isDashing = false;           
    public float dashSpeed = 25f;            
    public float dashDuration = 0.2f;        
    public float dashCooldown = 1f;          
    private float dashTime = 0f;             

    private float lastDashTime = -Mathf.Infinity;
    public void Dash()
    {
        if (UserInputManager.instance.DashInput && Time.time >= lastDashTime + dashCooldown)
        {
            isDashing = true;           
            dashTime = 0f;              
            lastDashTime = Time.time;   
            Debug.Log("Dash");          
        }

        if (isDashing)
        {
            dashTime += Time.deltaTime;
            if (dashTime >= dashDuration)
            {
                isDashing = false;
            }
        }
    }
}
