using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Turret : MonoBehaviour, IAttackable
{
    [SerializeField] protected float targetingRange = 5f;    // Raio de alcance para identificar inimigos.
    [SerializeField] protected LayerMask enemyMask;          // M�scara para identificar quais objetos s�o inimigos.
    [SerializeField] protected GameObject bulletPrefab;      // Prefab do proj�til que a torre dispara.
    [SerializeField] protected Transform firingPoint;        // Ponto de onde os proj�teis s�o disparados.
    [SerializeField] private float bulletsPerSecond = 1f;    // Taxa de disparo (disparos por segundo).

    protected Transform target;             
    private float timeUntilNextShot;         

    public virtual void Attack()    // M�todo virtual para atacar, podendo ser sobrescrito em classes derivadas.
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
                timeUntilNextShot = 0f;  // Reseta o tempo at� o pr�ximo disparo.
            }
        }
    }
    protected virtual void Shoot()    // M�todo protegido para disparar um proj�til.
    {
        // Instancia o proj�til no ponto de disparo e define o alvo.
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(target);
        }
    }

    private bool IsTargetInRange()    // Verifica se o alvo est� dentro do alcance da torre.
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

