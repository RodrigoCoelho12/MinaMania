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

    [SerializeField] float explosionRadius = 5f;

    public override void Attack()
    {
        IEnumerator LaunchBomb()
        {
            bombAmount--;
            bombInScene = true;

            for (int i = 0; i < bombIcons.Length; i++)
            {
                bombIcons[i].enabled = i < bombAmount;
            }

            bombRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(bombRay, out bombRayHit))
            {
                bombTarget = bombRayHit.point;
            }

            Vector3 bombLaunchPoint = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            GameObject _bomb = Instantiate(bombPrefab, bombLaunchPoint, Quaternion.identity);

            Vector3 control = (bombTarget + _bomb.transform.position) / 2;
            control.y += 6f;

            while (_bomb.transform.position != bombTarget)
            {
                Vector3 Evaluate(float t)
                {
                    Vector3 ac = Vector3.Lerp(bombLaunchPoint, control, t); // Lancar bomba com curvatura (lerp)
                    Vector3 cb = Vector3.Lerp(control, bombTarget, t);
                    return Vector3.Lerp(ac, cb, t);
                }

                sampleTime += Time.deltaTime * bombSpeed;
                _bomb.transform.position = Evaluate(sampleTime);
                _bomb.transform.forward = Evaluate(sampleTime + 0.001f) - _bomb.transform.position;

                yield return null;
            }

            GameObject _bombExplosion = Instantiate(bombExplosionPrefab, bombTarget, Quaternion.identity);

            RaycastHit[] hits = Physics.SphereCastAll(bombTarget, explosionRadius, Vector3.up, 0f);
            foreach (RaycastHit hit in hits)
            {
                var enemy = hit.collider.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.DeathRoutine();
                }
            }

            Destroy(_bombExplosion, 1f);
            Destroy(_bomb);

            bombInScene = false;
            sampleTime = 0;
        }

        if (UserInputManager.instance.BombInput && bombAmount > 0 && !bombInScene && !UserInputManager.instance.SprayInput)
        {
            StartCoroutine(LaunchBomb());
        }
    }
}
