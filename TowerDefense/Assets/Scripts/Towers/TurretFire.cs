using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretFire : Turret
{
    [SerializeField] private float burnDuration = 3f;          
    [SerializeField] private float burnDamagePerSecond = 1f;  

    public override void Attack()    // M�todo chamado para atacar o alvo.
    {
        if (target == null) return;

        Health enemyHealth = target.GetComponent<Health>(); // Obt�m o componente de sa�de do inimigo.

        if (enemyHealth != null) // Se o inimigo tiver um componente de sa�de, aplica queimadura.
        {
            StartCoroutine(ApplyBurnDamage(enemyHealth));
        }
    }
}
