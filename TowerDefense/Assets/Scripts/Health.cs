using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour // Classe Health: Gerencia os pontos de vida e a destruição de um objeto.

{
    [SerializeField] protected float hitPoints = 2;    // Pontos de vida iniciais do objeto. 

    protected bool isDestroyed = false;    // Indica se o objeto já foi destruído.

    public virtual void TakeDamage(float dmg)    // Método virtual para aplicar dano ao objeto.

    {
        hitPoints -= dmg;// Subtrai o dano dos pontos de vida.
        if (hitPoints <= 0 && !isDestroyed)        // Verifica se os pontos de vida chegaram a zero ou menos e se o objeto não foi destruído.

        {
            EnemySpawner.onEnemyDestroy.Invoke();// Notifica o spawner que um inimigo foi destruído.
            isDestroyed = true;// Marca o objeto como destruído.
            Destroy(gameObject);// Destrói o objeto do jogo.
        }
    }
}
