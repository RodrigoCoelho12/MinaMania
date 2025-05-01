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
        IEnumerator LaunchBomb()
        {
            bombAmount--;
            bombInScene = true;

            for (int i = 0; i < bombIcons.Length; i++)
            {
                bombIcons[i].enabled = i < bombAmount;
            }

            bombRay = Camera.main.ScreenPointToRay(Input.mousePosition); // Cria um Ray com origem da camera e direção determinada pela posição do mouse na tela
            if (Physics.Raycast(bombRay, out bombRayHit)) // Lança o Ray e armazena o hit (acerto) do  Raycast na variavel bombRayHit
            {
                bombTarget = bombRayHit.point; // Armazena a posição em Vector3 do local de acerto do Ray e armazena na variavel bombTarget
            }

            Vector3 bombLaunchPoint = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            GameObject _bomb = Instantiate(bombPrefab, bombLaunchPoint, Quaternion.identity);

            Vector3 pontoMedio = (bombTarget + _bomb.transform.position) / 2; // Variavel utilizada para armazenar o ponto médio entre a posição de origem e o alvo, onde será o ápice da parabola
            pontoMedio.y += 6f; // Determina o valor de altura do ápice da parabola

            while (_bomb.transform.position != bombTarget)
            {
                Vector3 Evaluate(float t) // Retorna o Lerp entre outros dois Lerps em função de um tempo t
                {
                    Vector3 ab = Vector3.Lerp(bombLaunchPoint, pontoMedio, t); // Cria um vetor que lerpa desde o ponto de lançamento da bomba até o ápice de maneira linear
                    Vector3 bc = Vector3.Lerp(pontoMedio, bombTarget, t); // Cria um vetor que lerpa desde o ápice da parabola até o alvo da bomba de maneira linear
                    return Vector3.Lerp(ab, bc, t); // Lerpa os outros dois lerps para garantir um movimento em arco de velocidade linear
                }

                sampleTime += Time.deltaTime * bombSpeed;// sampleTime é calculado para ser utilizado na movimentação da bomba baseando no deltaTime multiplicado pela velocidade
                _bomb.transform.position = Evaluate(sampleTime); // A posição da bomba é alterada em função de sampleTime, de acordo os lerps dentro do método
                _bomb.transform.forward = Evaluate(sampleTime + 0.001f) - _bomb.transform.position; // Faz com que a bomba alinhe seu vetor transform.forward de acordo com a parabola

                yield return null;
            }

            GameObject _bombExplosion = Instantiate(bombExplosionPrefab, bombTarget, Quaternion.identity);

            RaycastHit[] hits = Physics.SphereCastAll(bombTarget, atkRadius, Vector3.up, 0f);
            
            foreach (RaycastHit hit in hits)
            {
                var enemy = hit.collider.GetComponent<HealthController>();

                if (enemy != null)
                {
                    enemy.TakeDamage(atkValue);
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
