using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSlomo : Turret
{
    [SerializeField] private float attackRate = 4f;      // Ataques por segundo.
    [SerializeField] private float slowEffectDuration = 1f;  // Dura��o do efeito de redu��o de velocidade.
    [SerializeField] private float slowMultiplier = 0.5f;    // Multiplicador de redu��o de velocidade (ex.: 0.5f reduz a velocidade pela metade).

    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f / attackRate)
        {
            ApplySlowEffect();
            timeUntilFire = 0f; 
        }
    }
    private void ApplySlowEffect() 
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        foreach (var hit in hits)
        {
            EnemyMovement enemyMovement = hit.transform.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.UpdateSpeed(enemyMovement.baseSpeed * slowMultiplier); // Aplica a redu��o de velocidade.
                StartCoroutine(ResetEnemySpeed(enemyMovement)); // Reseta a velocidade ap�s a dura��o do efeito.
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement enemyMovement) // Corrotina para restaurar a velocidade do inimigo.
    {
        yield return new WaitForSeconds(slowEffectDuration);
        enemyMovement.ResetSpeed(); // Restaura a velocidade original do inimigo.
    }
}

