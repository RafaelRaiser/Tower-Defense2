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
        StartCoroutine(RegenerateHealthOverTime());  // Inicia o processo de regenera��o.
    }

    private IEnumerator RegenerateHealthOverTime()
    {
        while (!destroyed) // Continua regenerando enquanto o inimigo n�o estiver destru�do.
        {
            if (healthPoints < maxHealthPoints)
            {
                healthPoints += regenerationRate * Time.deltaTime; // Regenera sa�de de forma incremental.
                healthPoints = Mathf.Min(healthPoints, maxHealthPoints); // Limita ao valor m�ximo.
            }
            yield return null;  // Aguarda o pr�ximo frame antes de continuar.
        }
    }

    public override void ReceiveDamage(float damageAmount)
    {
        base.ReceiveDamage(damageAmount); // Aplica a l�gica de dano da classe base.
    }
}
