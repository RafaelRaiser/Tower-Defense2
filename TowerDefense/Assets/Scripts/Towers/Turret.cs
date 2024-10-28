using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Turret : MonoBehaviour, IAttackable
{
    [SerializeField] protected float targetingRange = 5f;    // Raio de alcance para identificar inimigos.
    [SerializeField] protected LayerMask enemyMask;          // Máscara para identificar quais objetos são inimigos.
    [SerializeField] protected GameObject bulletPrefab;      // Prefab do projétil que a torre dispara.
    [SerializeField] protected Transform firingPoint;        // Ponto de onde os projéteis são disparados.
    [SerializeField] private float bulletsPerSecond = 1f;    // Taxa de disparo (disparos por segundo).

    protected Transform target;             
    private float timeUntilNextShot;         

    public virtual void Attack()    // Método virtual para atacar, podendo ser sobrescrito em classes derivadas.
    {

    }
    private void Update()
    {
        if (target == null)
        {
            LocateTarget();
            return;
        }

        if (!IsTargetInRange())
        {
            target = null;  // Perde o alvo se ele sair do alcance.
        }
        else
        {
            timeUntilNextShot += Time.deltaTime;
            if (timeUntilNextShot >= 1f / bulletsPerSecond)
            {
                Shoot();
                timeUntilNextShot = 0f;  // Reseta o tempo até o próximo disparo.
            }
        }
    }
    protected virtual void Shoot()    // Método protegido para disparar um projétil.
    {
        // Instancia o projétil no ponto de disparo e define o alvo.
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(target);
        }
    }

    private bool IsTargetInRange()    // Verifica se o alvo está dentro do alcance da torre.
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void LocateTarget()    // Procura um alvo inimigo dentro do alcance usando um CircleCast.
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

}

