using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSlomo : Turret
{
    [SerializeField] private float attackRate = 4f;      // Ataques por segundo.
    [SerializeField] private float slowEffectDuration = 1f;  // Duração do efeito de redução de velocidade.
    [SerializeField] private float slowMultiplier = 0.5f;    // Multiplicador de redução de velocidade (ex.: 0.5f reduz a velocidade pela metade).

    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f / attackRate)
        {
            ApplySlowEffect();
            timeUntilFire = 0f; 
        }
    }

}

