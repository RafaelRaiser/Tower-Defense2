using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, Iatacavel, IBuscadorDeAlvo
{
    [SerializeField] protected float targetingRange = 5f;
    [SerializeField] protected LayerMask enemyMask;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firingPoint;
    [SerializeField] private float bps = 1f;

    protected Transform target; // Alvo atual.
    protected float timeUntilFire; // Tempo at� o pr�ximo tiro.

    public virtual void Atacar() { }

    private void Update()
    {
        if (target == null) target = ObterAlvo();
        else if (!CheckTargetIsInRange()) target = null;
        else
        {
            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    protected virtual void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    public Transform ObterAlvo()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);
        return hits.Length > 0 ? hits[0].transform : null;
    }
}
