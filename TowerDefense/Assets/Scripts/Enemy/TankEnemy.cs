using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : HealthSystem
{
    [Header("Tank Enemy Settings")]
    [SerializeField] private float resistanceMultiplier = 0.5f; // Diminui o dano recebido.

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage * resistanceMultiplier); // Toma menos dano.
    }
}
