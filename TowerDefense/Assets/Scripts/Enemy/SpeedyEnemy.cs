using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedyEnemy : HealthSystem
{
    [Header("Speedy Enemy Settings")]
    [SerializeField] private float lowHealthMultiplier = 0.5f; // Reduz os pontos de vida para compensar a velocidade.

    private void Start()
    {
        healthPoints = Mathf.Max(1, healthPoints * lowHealthMultiplier); // Define vida reduzida.
    }
}
