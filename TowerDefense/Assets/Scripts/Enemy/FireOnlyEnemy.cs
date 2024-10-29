using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOnlyEnemy : HealthSystem
{
    public override void TakeDamage(float damage, string sourceType)
    {
        if (sourceType == "Fire")
        {
            base.TakeDamage(damage); // Toma dano apenas de torres de fogo.
        }
    }
}
