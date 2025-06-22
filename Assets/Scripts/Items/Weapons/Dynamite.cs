using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Dynamite : Weapon
{
    [Header("Dynamite Amount Properties")]
    private int dynamiteCurrentAmount;

    [Header("Dynamite Launch Properties")]
    private Vector3 dynamitePositionLaunchFactor = new Vector3(0f, 2f, 0f);
    private readonly float arcHeight = 10f;
    private bool isLaunching = false;

    [Header("Dynamite UI Properties")] 
    public List<Image> dynamiteIcons;

    [Header("Dynamite Data Properties")]
    public DynamiteData dynamiteData;
    
    [Header("Dynamite Reference Properties")]
    private GameObject player;

    [Header("Dynamite Raycast Properties")]
    public LayerMask explosionLayerMask;
    public RaycastHit[] explosionHits;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        dynamiteCurrentAmount = dynamiteData.dynamiteAmount;
    }

    public override void Attack()
    {
        if (UserInputManager.instance.dynamiteInput && dynamiteCurrentAmount > 0 && !isLaunching)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                StartCoroutine(LaunchDynamite(hit.point));
            }
        }
    }

    private IEnumerator LaunchDynamite(Vector3 landingPosition)
    {
        isLaunching = true;

        GameObject dynamite = GameObject.Instantiate(dynamiteData.dynamitePrefab, player.transform.position + dynamitePositionLaunchFactor, Quaternion.identity);

        dynamiteCurrentAmount--;
        UpdateDynamiteUI();
        Vector3 startPosition = dynamite.transform.position + dynamitePositionLaunchFactor;
        float elapsedTime = 0f;
        float duration = 1.0f;


        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Vector3 horizontalPos = Vector3.Lerp(startPosition, landingPosition, t);
            float height = 4 * arcHeight * t * (1 - t);
            horizontalPos.y += height;
            dynamite.transform.position = horizontalPos;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        dynamite.transform.position = landingPosition;
        isLaunching = false;
        Explode(dynamite);
    }

    private void Explode(GameObject dynamite)
    {
        Destroy(dynamite);
        if (Physics.CheckSphere(dynamite.transform.position, dynamiteData.explosionRadius))
        {
            Debug.DrawLine(dynamite.transform.position, Vector3.up * dynamiteData.explosionRadius, Color.cyan, 2f);
            explosionHits = Physics.SphereCastAll(dynamite.transform.position, dynamiteData.explosionRadius, Vector3.up, 0f);
            foreach (RaycastHit hit in explosionHits)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    GameObject enemy = hit.collider.gameObject;
                    Rigidbody rb = enemy.GetComponent<Rigidbody>();

                    enemy.GetComponent<Enemy>().healthBar.AdjustStatusBarBySubtraction(dynamiteData.damage);
                    enemy.GetComponent<Enemy>().CheckDeath();

                    enemy.GetComponent<NavMeshAgent>().enabled = false;

                    rb.AddExplosionForce(dynamiteData.explosionForce, dynamite.transform.position, dynamiteData.explosionRadius, 0f,ForceMode.Impulse);
                }
            }
        }
    }
    private void UpdateDynamiteUI()
    {
        for (int i = 0; i < dynamiteIcons.Count; i++)
        {
            dynamiteIcons[i].enabled = i < dynamiteCurrentAmount;
        }
    }
}
