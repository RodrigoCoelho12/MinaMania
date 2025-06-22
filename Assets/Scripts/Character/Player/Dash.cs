using UnityEngine;

public partial class Player : Character
{
    [Header("Dash Properties")]
    public bool isDashing = false;           
    private float dashTime = 0f; 
    private float lastDashTime = -Mathf.Infinity;

    private int dashCount = 0;

    public void Dash()
    {
        if (UserInputManager.instance.DashInput && Time.time >= lastDashTime + currentDashData.dashCooldown)
        {
            isDashing = true;           
            dashTime = 0f;              
            lastDashTime = Time.time;   
            Debug.Log("Dash");     
            
            dashCount++;
            AnalyticsTest.Instance.AddAnalytics("Player", "Dash", dashCount.ToString());
        }

        if (isDashing)
        {
            dashTime += Time.deltaTime;
            if (dashTime >= currentDashData.dashDuration)
            {
                isDashing = false;
            }
        }
    }
}
