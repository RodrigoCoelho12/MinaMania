using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BombLauncher : Weapon
{
    private Ray bombRay;
    private RaycastHit bombRayHit;
    private Vector3 bombTarget;

    private bool bombInScene;
    private float bombSpeed = 1f;
    private float sampleTime;

    [SerializeField] int bombAmount = 4;
    [SerializeField] GameObject bombPrefab;
    [SerializeField] GameObject bombExplosionPrefab;
    [SerializeField] Image[] bombIcons;

    public override void Attack()
    {
        if (CanLaunchBomb())
        {
            StartCoroutine(LaunchBomb());
        }
    }

    private bool CanLaunchBomb()
    {
        return UserInputManager.instance.BombInput &&
               bombAmount > 0 &&
               !bombInScene &&
               !UserInputManager.instance.SprayInput;
    }

    private IEnumerator LaunchBomb()
    {
        bombAmount--;
        bombInScene = true;
        UpdateUI();

        SetBombTarget();

        Vector3 launchPoint = GetLaunchPoint();
        GameObject bomb = Instantiate(bombPrefab, launchPoint, Quaternion.identity);
        Vector3 midPoint = GetParabolaMidPoint(launchPoint);

        yield return MoveBombAlongParabola(bomb, launchPoint, midPoint, bombTarget);

        GameObject explosion = Instantiate(bombExplosionPrefab, bombTarget, Quaternion.identity);
        DamageEnemies();
        Destroy(explosion, 1f);
        Destroy(bomb);

        bombInScene = false;
        sampleTime = 0;
    }

    private void UpdateUI()
    {
        for (int i = 0; i < bombIcons.Length; i++)
        {
            bombIcons[i].enabled = i < bombAmount;
        }
    }

    private void SetBombTarget()
    {
        bombRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(bombRay, out bombRayHit))
        {
            bombTarget = bombRayHit.point;
        }
    }

    private Vector3 GetLaunchPoint()
    {
        return new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
    }

    private Vector3 GetParabolaMidPoint(Vector3 start)
    {
        Vector3 mid = (bombTarget + start) / 2;
        mid.y += 6f;
        return mid;
    }

    private IEnumerator MoveBombAlongParabola(GameObject bomb, Vector3 start, Vector3 mid, Vector3 end)
    {
        while (bomb.transform.position != end)
        {
            Vector3 ab = Vector3.Lerp(start, mid, sampleTime);
            Vector3 bc = Vector3.Lerp(mid, end, sampleTime);
            Vector3 position = Vector3.Lerp(ab, bc, sampleTime);

            bomb.transform.position = position;
            bomb.transform.forward = Vector3.Lerp(ab, bc, sampleTime + 0.001f) - position;

            sampleTime += Time.deltaTime * bombSpeed;
            yield return null;
        }
    }

    private void DamageEnemies()
    {
        RaycastHit[] hits = Physics.SphereCastAll(bombTarget, atkRadius, Vector3.up, 0f);

        foreach (RaycastHit hit in hits)
        {
            var enemy = hit.collider.GetComponent<HealthController>();
            if (enemy != null)
            {
                enemy.TakeDamage(atkValue);
            }
        }
    }
}
