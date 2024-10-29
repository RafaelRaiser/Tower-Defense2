using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireResistantEnemy : HealthSystem
{
    [SerializeField] private float slomoDamageMultiplier = 1.5f; // Multiplicador de dano para torre Slomo.

    public override void TakeDamage(float damage, string sourceType)
    {
        if (sourceType == "Fire")
        {
            return; // Não toma dano de torres de fogo.
        }
        else if (sourceType == "Slomo")
        {
            base.TakeDamage(damage * slomoDamageMultiplier); // Toma dano aumentado das torres Slomo.
        }
        else
        {
            base.TakeDamage(damage); // Toma dano normal de outras fontes.
        }
    }
}