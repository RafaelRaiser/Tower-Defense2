using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, Iatacavel 

{
    [SerializeField] protected float targetingRange = 5f;    
    [SerializeField] protected LayerMask enemyMask;    
    [SerializeField] protected GameObject bulletPrefab;    
    [SerializeField] protected Transform firingPoint;    
    [SerializeField] private float bps = 1f;  
    
    protected Transform target;    
    protected float timeUntilFire;    


    public virtual void Atacar()    

    {
        //implementa��o classes derivadas.
    }
    
   private void Update()    // M�todo chamado a cada quadro para verificar e atacar inimigos.

    {
        if (target == null)        // Se n�o houver alvo, procura um.

        {
            FindTarget();
            return; 
        }
        if (!CheckTargetIsInRange())
        {
            target = null;        // Verifica se o alvo est� fora do alcance.

        }
        else
        {
            timeUntilFire += Time.deltaTime;            // Aumenta o tempo at� o pr�ximo disparo.

            if (timeUntilFire >= 1f / bps)            // Verifica se � hora de disparar.

            {
                Shoot();// Realiza o disparo.
                timeUntilFire = 0f;  // Reseta o tempo at� o pr�ximo disparo.
            }
        }
        
    }
    protected virtual void Shoot()    // M�todo protegido para disparar um proj�til.

    {
        GameObject bulletObj = Instantiate(bulletPrefab,firingPoint.position,Quaternion.identity);        // Instancia um objeto de bala na posi��o do ponto de disparo.
        
        // Obt�m o script da bala e define o alvo.
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }
    private bool CheckTargetIsInRange()    // Verifica se o alvo est� dentro do alcance da torre.

    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }
    private void FindTarget()    // Procura por um alvo inimigo dentro do alcance usando um CircleCast.

    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
        
        // Se houver inimigos detectados, define o primeiro como alvo.
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }
}
