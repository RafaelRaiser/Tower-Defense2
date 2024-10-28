using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] protected float healthPoints = 2f;  // Pontos de vida iniciais do objeto.

    protected bool destroyed = false;   // Flag para verificar se o objeto foi destruído.

    // Método virtual para receber dano. Pode ser sobrescrito em subclasses para comportamento personalizado.
    public virtual void ReceiveDamage(float damageAmount)
    {
        if (destroyed) return; // Ignora o dano se o objeto já estiver destruído.

        healthPoints -= damageAmount; // Aplica o dano aos pontos de vida.

        if (healthPoints <= 0)
        {
            HandleDestruction();
        }
    }

    // Lida com a destruição do objeto.
    protected virtual void HandleDestruction()
    {
        destroyed = true; // Marca o objeto como destruído.
        EnemySpawner.onEnemyDestroy?.Invoke(); // Notifica que o objeto foi destruído, com verificação de ouvintes.

        Destroy(gameObject); // Remove o objeto da cena.
    }
}

