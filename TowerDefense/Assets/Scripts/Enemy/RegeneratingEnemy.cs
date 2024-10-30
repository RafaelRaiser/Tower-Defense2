using System.Collections;
using UnityEngine;

public class RegeneratingEnemy : Health 
{
    [SerializeField] private float regenerationRate =2f;   
    [SerializeField] private float maxHits = 10f;   

    private void Start()     

    {
        hit = maxHits;   // Define os pontos de vida iniciais como o máximo.
        StartCoroutine(RegenerateHealth());  // Inicia a corrotina para regenerar saúde.
    }

    private IEnumerator RegenerateHealth()     // Corrotina que lida com a regeneração da saúde do inimigo.

    {
        while (!isDestroyed)         // Enquanto o inimigo não estiver destruído, continua a regenerar saúde.

        {
            if (hitPoints < maxHits)             // Se os pontos de vida estiverem abaixo do máximo, aumenta os pontos de vida.

            {
                hitPoints += regenerationRate * Time.deltaTime;  // Regenera saúde com base na taxa e no tempo.
                hitPoints = Mathf.Min(hitPoints, maxHits);   // Garante que os pontos de vida não ultrapassem o máximo.
            }
            yield return null;  // Espera o próximo quadro antes de continuar.
        }
    }

    public override void TakeDamage(float dmg)  // Método chamado quando o inimigo recebe dano.
    {
        base.TakeDamage(dmg);  // Chama o método TakeDamage da classe base.
    }
}
