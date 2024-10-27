using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Rigidbody2D enemyRigidbody;  // Componente para aplicar a f�sica no inimigo.
    [SerializeField] private float movementSpeed = 2f;    // Velocidade atual do movimento.

    private Transform currentTarget;   // Alvo atual para onde o inimigo est� indo.
    private int currentPathIndex = 0;  // �ndice do caminho atual do inimigo.
    private float defaultSpeed;        // Armazena a velocidade padr�o do inimigo para ser resetada.

    private void Start()
    {
        InitializeMovementSettings();
    }

    private void InitializeMovementSettings()
    {
        defaultSpeed = movementSpeed;  // Define a velocidade padr�o inicial.
        currentTarget = LevelManager.instance.path[currentPathIndex]; // Primeiro ponto do caminho como alvo.
    }

    private void Update()
    {
        CheckProximityToTarget();
    }

    private void CheckProximityToTarget()
    {
        if (Vector2.Distance(transform.position, currentTarget.position) <= 0.1f)
        {
            SetNextTarget();
        }
    }
}