using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretFire : Turret
{
    [SerializeField] private float burnDuration = 3f;          
    [SerializeField] private float burnDamagePerSecond = 1f;  

    public override void Attack()    // Método chamado para atacar o alvo.
    {
        if (target == null) return;

        Health enemyHealth = target.GetComponent<Health>(); // Obtém o componente de saúde do inimigo.

        if (enemyHealth != null) // Se o inimigo tiver um componente de saúde, aplica queimadura.
        {
            StartCoroutine(ApplyBurnDamage(enemyHealth));
        }
    }
    private IEnumerator ApplyBurnDamage(Health enemyHealth)    // Corrotina que aplica dano por queimadura ao longo do tempo.
    {
        float elapsedTime = 0f;

        while (elapsedTime < burnDuration) // Aplica dano enquanto a queimadura durar.
        {
            enemyHealth.TakeDamage(burnDamagePerSecond * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;   // Espera até o próximo quadro.
        }
    }

}
