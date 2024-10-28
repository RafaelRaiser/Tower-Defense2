using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RegeneratingEnemy : HealthSystem
{
    [SerializeField] private float regenerationRate = 1f;
    [SerializeField] private float maxHealthPoints = 5f;      

    private void Start()
    {
        healthPoints = maxHealthPoints;         // Define os pontos de vida iniciais.
        StartCoroutine(RegenerateHealthOverTime());  // Inicia o processo de regeneração.
    }

    private IEnumerator RegenerateHealthOverTime()
    {
        while (!destroyed) // Continua regenerando enquanto o inimigo não estiver destruído.
        {
            if (healthPoints < maxHealthPoints)
            {
                healthPoints += regenerationRate * Time.deltaTime; // Regenera saúde de forma incremental.
                healthPoints = Mathf.Min(healthPoints, maxHealthPoints); // Limita ao valor máximo.
            }
            yield return null;  // Aguarda o próximo frame antes de continuar.
        }
    }

    public override void ReceiveDamage(float damageAmount)
    {
        base.ReceiveDamage(damageAmount); // Aplica a lógica de dano da classe base.
    }
}
