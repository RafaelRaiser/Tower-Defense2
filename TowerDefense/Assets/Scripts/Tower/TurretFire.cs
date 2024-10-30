using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretFire : Turret .

{
    [SerializeField] private float burnDuration = 3f;      
    [SerializeField] private float burnDamagePerSecond = 1f;    

    public override void Atacar()     

    {
        if (target != null)        

        {

            Health enemyHealth = target.GetComponent<Health>();             // Obtém  componente de saúde do inimigo.


            if (enemyHealth != null)             // Se o inimigo tiver um componente de saúde inicia a aplicação de dano por queimadura.

            {

                StartCoroutine(ApplyBurnDamage(enemyHealth));
            }
        }
    }
    private IEnumerator ApplyBurnDamage(Health enemyHealth)     

    {
        float elapsedTime = 0f; // Tempo 

        while (elapsedTime < burnDuration) // Enquanto o tempo  for menor que a duração da queimadura.
        {
            enemyHealth.TakeDamage(burnDamagePerSecond * Time.deltaTime);    // Aplica dano ao inimigo baseado no dano por segundo.
            elapsedTime += Time.deltaTime; // Atualiza o tempo decorrido.
            yield return null;   // Espera o próximo quadro antes de continuar.
        }
    }

    
    protected override void Shoot()     

    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);         // Instancia o objeto da bala na posição do ponto de disparo.

        // Obtém o script da bala e define o alvo.

        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);

        Atacar();          // Chama o método de ataque para aplicar queimadura.

    }
}

