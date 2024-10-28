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
}

