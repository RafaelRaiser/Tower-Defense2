using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class TurretSlomo : Turret, Iatacavel
{
    [SerializeField] private float aps = 4f;
    [SerializeField] private float FreezeTime = 1f;

    private void Update()
    {
        timeUntilFire += Time.deltaTime;
        if (timeUntilFire >= 1f / aps)
        {
            FreezeEnemies();
            timeUntilFire = 0f;
        }
    }

    private void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);
        foreach (var hit in hits)
        {
            EnemyMover em = hit.transform.GetComponent<EnemyMover>();
            if (em != null)
            {
                em.UpdateSpeed(0.5f);
                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMover em)
    {
        yield return new WaitForSeconds(FreezeTime);
        em.ResetSpeed();
    }
}

