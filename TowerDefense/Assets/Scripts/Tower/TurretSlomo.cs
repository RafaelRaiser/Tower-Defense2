using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretSlomo : Turret 

{

    [SerializeField] private float aps = 4f;    
    [SerializeField] private float FreezeTime = 1f;    

    private void Update()    

    {
        timeUntilFire += Time.deltaTime;       

        if (timeUntilFire >= 1f / aps)       

        {
            FreezeEnemies();// Aplica o efeito de congelamento aos inimigos.
            timeUntilFire = 0f;// Reseta o tempo até o próximo disparo.
        }

    }
    private void FreezeEnemies() // Método que congela inimigos dentro do alcance da torre.
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);   
        
        if (hits.Length > 0)        // Se houver inimigos detectados, aplica o efeito de congelamento.

        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();                // Obtém o componente de movimento do inimigo.

                em.UpdateSpeed(0.5f);                // Reduz a velocidade do inimigo.

                StartCoroutine(ResetEnemySpeed(em));                // Inicia a corrotina para resetar a velocidade após o tempo de congelamento.

            }
        }
    }
    private IEnumerator ResetEnemySpeed(EnemyMovement em)     //reseta a velocidade do inimigo após o tempo de congelamento.

    {
        yield return new WaitForSeconds(FreezeTime);        // Espera pelo tempo de congelamento.

        em.ResetSpeed();        // Reseta a velocidade do inimigo ao seu valor original.

    }
}
