using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretFire : Turret // Classe TurretFire: Representa uma torre que causa dano por queimadura a inimigos.

{
    [SerializeField] private float burnDuration = 3f;       // Duração da queimadura em segundos.

    [SerializeField] private float burnDamagePerSecond = 1f;     // Dano causado por segundo pela queimadura.

    public override void Atacar()     // Método chamado para atacar o alvo.

    {
        if (target != null)         // Verifica se há um alvo definido.

        {

            Health enemyHealth = target.GetComponent<Health>();             // Obtém o componente de saúde do inimigo.


            if (enemyHealth != null)             // Se o inimigo tiver um componente de saúde, inicia a aplicação de dano por queimadura.

            {

                StartCoroutine(ApplyBurnDamage(enemyHealth));
            }
        }
    }
    private IEnumerator ApplyBurnDamage(Health enemyHealth)     // Corrotina que aplica dano por queimadura ao inimigo ao longo do tempo.

    {
        float elapsedTime = 0f; // Tempo decorrido.

        while (elapsedTime < burnDuration) // Enquanto o tempo decorrido for menor que a duração da queimadura.
        {
            enemyHealth.TakeDamage(burnDamagePerSecond * Time.deltaTime);    // Aplica dano ao inimigo baseado no dano por segundo.
            elapsedTime += Time.deltaTime; // Atualiza o tempo decorrido.
            yield return null;   // Espera o próximo quadro antes de continuar.
        }
    }

    
    protected override void Shoot()     // Método protegido para atirar um projétil.

    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);         // Instancia o objeto da bala na posição do ponto de disparo.

        // Obtém o script da bala e define o alvo.

        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);

        Atacar();          // Chama o método de ataque para aplicar queimadura.

    }
}

