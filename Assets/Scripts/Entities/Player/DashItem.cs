using UnityEngine;

public class DashItem : Item
{
    public bool isDashing = false;
    public float dashSpeed = 25f;          
    public float dashCooldown = 1f;         
    public float dashDuration = 0.2f;        
    private float dashTime = 0f;             
    private float lastDashTime = -Mathf.Infinity;

    private void Update()
    {
        Dash();
    }
    public void Dash()
    {
        if (UserInputManager.instance.DashInput && Time.time >= lastDashTime + dashCooldown)
        {
            dashTime = 0f;
            isDashing = true;
            lastDashTime = Time.time;
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
