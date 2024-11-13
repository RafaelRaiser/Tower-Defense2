using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretSlomo : Turret, Iatacavel
{
    [SerializeField] private float aps = 4f; // Taxa de ataque por segundo.
    [SerializeField] private float FreezeTime = 1f; // Duração do congelamento.

    private void Update()
    {
        timeUntilFire += Time.deltaTime;
        if (timeUntilFire >= 1f / aps)
        {
            FreezeEnemies();
            timeUntilFire = 0f;
        }
    }

    private void FreezeEnemies()
    {
        // Obtém todos os inimigos na área de alcance.
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);
        foreach (var hit in hits)
        {
            EnemyMover em = hit.transform.GetComponent<EnemyMover>();
            if (em != null)
            {
                em.UpdateSpeed(0.5f); // Reduz a velocidade do inimigo.
                StartCoroutine(ResetEnemySpeed(em)); // Reseta a velocidade após o tempo de congelamento.
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMover em)
    {
        yield return new WaitForSeconds(FreezeTime);
        em.ResetSpeed(); // Reseta a velocidade do inimigo.
    }
}
