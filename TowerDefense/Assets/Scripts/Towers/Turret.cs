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
}

