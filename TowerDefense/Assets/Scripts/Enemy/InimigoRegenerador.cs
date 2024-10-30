using System.Collections;
using UnityEngine;

public class RegeneratingEnemy : Health // Classe RegeneratingEnemy: Representa um inimigo que regenera saúde ao longo do tempo, herdando de Health.

{
    [SerializeField] private float regenerationRate = 1f;   // Taxa de regeneração de saúde por segundo.
    [SerializeField] private float maxHitPoints = 5f;   // Máximo de pontos de vida que o inimigo pode ter.

    private void Start()     // Método chamado no início do jogo. Inicializa os pontos de vida e inicia o processo de regeneração.

    {
        hitPoints = maxHitPoints;   // Define os pontos de vida iniciais como o máximo.
        StartCoroutine(RegenerateHealth());  // Inicia a corrotina para regenerar saúde.
    }

    private IEnumerator RegenerateHealth()     // Corrotina que lida com a regeneração da saúde do inimigo.

    {
        while (!isDestroyed)         // Enquanto o inimigo não estiver destruído, continua a regenerar saúde.

        {
            if (hitPoints < maxHitPoints)             // Se os pontos de vida estiverem abaixo do máximo, aumenta os pontos de vida.

            {
                hitPoints += regenerationRate * Time.deltaTime;  // Regenera saúde com base na taxa e no tempo.
                hitPoints = Mathf.Min(hitPoints, maxHitPoints);   // Garante que os pontos de vida não ultrapassem o máximo.
            }
            yield return null;  // Espera o próximo quadro antes de continuar.
        }
    }

    public override void TakeDamage(float dmg)  // Método chamado quando o inimigo recebe dano.
    {
        base.TakeDamage(dmg);  // Chama o método TakeDamage da classe base.
    }
}
