using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFire : Turret
{
    [SerializeField] private float burnDuration = 3f; // Duração do efeito de queimadura.
    [SerializeField] private float burnDamagePerSecond = 1f; // Dano por segundo da queimadura.

    public override void Atacar()
    {
        if (target != null)
        {
            Health enemyHealth = target.GetComponent<Health>();
            if (enemyHealth != null)
            {
                StartCoroutine(ApplyBurnDamage(enemyHealth)); // Aplica o efeito de queimadura.
            }
        }
    }

    private IEnumerator ApplyBurnDamage(Health enemyHealth)
    {
        float elapsedTime = 0f;
        while (elapsedTime < burnDuration)
        {
            enemyHealth.Damaged(burnDamagePerSecond * Time.deltaTime); // Aplica dano ao inimigo.
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    protected override void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
        Atacar(); // Chama o método Atacar.
    }
}
