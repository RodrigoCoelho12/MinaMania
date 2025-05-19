using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BombLauncher : Weapon
{
    private Ray bombRay;
    private RaycastHit bombRayHit;
    private Vector3 bombTarget;

    private bool bombInScene; // Indica se há uma bomba ativa na cena (impede lançar mais de uma ao mesmo tempo)

    private float bombSpeed = 1f; // Velocidade de movimento da bomba na parábola
    private float sampleTime;     // Tempo de amostragem para o cálculo da curva

    [SerializeField] int bombAmount = 4; // Quantidade inicial de bombas

    [SerializeField] GameObject bombPrefab;           // Prefab da bomba que será lançada
    [SerializeField] GameObject bombExplosionPrefab;  // Prefab da explosão da bomba
    [SerializeField] Image[] bombIcons;               // Ícones da HUD que representam as bombas restantes

    public override void Attack()
    {
        IEnumerator LaunchBomb() // Corrotina responsável por todo o processo de lançamento da bomba
        {
            bombAmount--; // Diminui uma bomba do total ao lançar
            bombInScene = true; // Marca que existe uma bomba ativa na cena

            // Atualiza os ícones de bomba na UI, desativando os que excedem a quantidade atual
            for (int i = 0; i < bombIcons.Length; i++)
            {
                bombIcons[i].enabled = i < bombAmount;
            }

            bombRay = Camera.main.ScreenPointToRay(Input.mousePosition); // Cria um Ray com origem da camera e dire��o determinada pela posi��o do mouse na tela
            if (Physics.Raycast(bombRay, out bombRayHit)) // Lan�a o Ray e armazena o hit (acerto) do  Raycast na variavel bombRayHit
            {
                bombTarget = bombRayHit.point; // Armazena a posi��o em Vector3 do local de acerto do Ray e armazena na variavel bombTarget
            }

            // Define o ponto inicial do lançamento da bomba (um pouco acima da posição do jogador)
            Vector3 bombLaunchPoint = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

            // Instancia o prefab da bomba nesse ponto
            GameObject _bomb = Instantiate(bombPrefab, bombLaunchPoint, Quaternion.identity);

            Vector3 pontoMedio = (bombTarget + _bomb.transform.position) / 2; // Variavel utilizada para armazenar o ponto m�dio entre a posi��o de origem e o alvo, onde ser� o �pice da parabola
            pontoMedio.y += 6f; // Determina o valor de altura do �pice da parabola

            // Enquanto a bomba ainda não tiver alcançado exatamente o alvo
            while (_bomb.transform.position != bombTarget)
            {
                Vector3 Evaluate(float t) // Retorna o Lerp entre outros dois Lerps em fun��o de um tempo t
                {
                    Vector3 ab = Vector3.Lerp(bombLaunchPoint, pontoMedio, t); // Cria um vetor que lerpa desde o ponto de lan�amento da bomba at� o �pice de maneira linear
                    Vector3 bc = Vector3.Lerp(pontoMedio, bombTarget, t); // Cria um vetor que lerpa desde o �pice da parabola at� o alvo da bomba de maneira linear
                    return Vector3.Lerp(ab, bc, t); // Lerpa os outros dois lerps para garantir um movimento em arco de velocidade linear
                }

                sampleTime += Time.deltaTime * bombSpeed; // sampleTime � calculado para ser utilizado na movimenta��o da bomba baseando no deltaTime multiplicado pela velocidade
                _bomb.transform.position = Evaluate(sampleTime); // A posi��o da bomba � alterada em fun��o de sampleTime, de acordo os lerps dentro do m�todo
                _bomb.transform.forward = Evaluate(sampleTime + 0.001f) - _bomb.transform.position; // Faz com que a bomba alinhe seu vetor transform.forward de acordo com a parabola

                yield return null; // Espera um frame antes de continuar o loop
            }

            // Instancia o efeito de explosão no ponto final da trajetória
            GameObject _bombExplosion = Instantiate(bombExplosionPrefab, bombTarget, Quaternion.identity);

            // Cria uma esfera invisível no local da explosão para detectar inimigos próximos
            RaycastHit[] hits = Physics.SphereCastAll(bombTarget, atkRadius, Vector3.up, 0f);

            foreach (RaycastHit hit in hits)
            {
                var enemy = hit.collider.GetComponent<HealthController>(); // Tenta acessar o componente de vida

                if (enemy != null && enemy.CompareTag("Enemy"))
                {
                    enemy.TakeDamage(atkValue); // Causa dano ao inimigo caso ele tenha o componente
                }
            }

            Destroy(_bombExplosion, 1f); // Destroi a explosão visual após 1 segundo
            Destroy(_bomb); // Remove o objeto bomba

            bombInScene = false; // Permite lançar outra bomba novamente
            sampleTime = 0; // Reseta o tempo da parábola
        }

        // Condição para lançar bomba:
        // - O jogador apertou o botão de bomba
        // - Ainda tem bomba disponível
        // - Nenhuma bomba já está ativa
        // - O botão de ataque alternativo (Spray) não está sendo pressionado
        if (UserInputManager.instance.BombInput && bombAmount > 0 && !bombInScene && !UserInputManager.instance.SprayInput)
        {
            StartCoroutine(LaunchBomb()); // Inicia a corrotina de lançamento da bomba
        }
    }
}
