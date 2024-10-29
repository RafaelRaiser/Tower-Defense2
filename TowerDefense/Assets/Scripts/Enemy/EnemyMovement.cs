using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMovement : MonoBehaviour // Classe EnemyMovement: Controla o movimento dos inimigos ao longo de um caminho definido.

{
    [SerializeField] private Rigidbody2D rb;    // Componente Rigidbody2D usado para movimentar o inimigo.

    [SerializeField] private float moveSpeed = 2f;    // Velocidade de movimento do inimigo.

    private Transform target;    // Referência ao próximo ponto do caminho que o inimigo deve seguir.

    private int pathIndex = 0;    // Índice do ponto atual no caminho.

    private float baseSpeed;    // Velocidade base do inimigo, usada para resetar a velocidade.

    // Método chamado no início do jogo. Inicializa a velocidade e o alvo do inimigo.
    private void Start()
    {
        baseSpeed = moveSpeed; // Armazena a velocidade base.
        target = LevelManager.instance.path[pathIndex]; // Define o primeiro ponto do caminho como alvo.
    }

    // Método chamado a cada quadro para verificar a distância até o próximo ponto do caminho.
    private void Update()
    {
        if (Vector2.Distance(target.position,transform.position) <= 0.1f)         // Verifica se o inimigo chegou perto o suficiente do ponto alvo.

        {
            pathIndex++; // Avança para o próximo ponto do caminho.

            if (pathIndex == LevelManager.instance.path.Length)            // Verifica se o inimigo atingiu o final do caminho.

            {
                EnemySpawner.onEnemyDestroy.Invoke(); // Notifica o spawner que o inimigo foi destruído.
                Destroy(gameObject); // Destrói o objeto do inimigo.
                return;
            }
            else
            {
                target = LevelManager.instance.path[pathIndex]; // Atualiza o alvo para o próximo ponto do caminho.
            }
        }
    }
    private void FixedUpdate()     // Método chamado a cada quadro de física para mover o inimigo.

    {
        Vector2 direction = (target.position -  transform.position).normalized;         // Calcula a direção do movimento em direção ao alvo e normaliza.


        rb.velocity = direction * moveSpeed;         // Define a velocidade do Rigidbody2D para mover o inimigo em direção ao alvo.

    }
    public void UpdateSpeed(float newSpeed)     // Método para atualizar a velocidade do inimigo, usado por torres ou outros efeitos.

    {
        moveSpeed = newSpeed; // Atualiza a velocidade de movimento.
    }
    public void ResetSpeed()     // Método para resetar a velocidade do inimigo à sua velocidade base.

    {
        moveSpeed = baseSpeed; // Restaura a velocidade original.
    }
}
