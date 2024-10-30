using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemySpawner : MonoBehaviour 

{
    [SerializeField] private GameObject[] enemyPrefabs;    

    [SerializeField] private int perEnemiesWave = 12;    
    [SerializeField] private float enemiesPerSecond = 0.5f; 
    [SerializeField] private float timeBetweenWaves = 5f;    
    [SerializeField] private float difficultyScalingFactor = 0.75f;    
    [SerializeField] private float enemiesPerSecondCap = 15f;    


    public static UnityEvent onEnemyDestroy = new UnityEvent();    

    private int currentWave = 1;
    private float timeSinceLastSpawn; 
    private int enemiesAlive; 
    private int enemiesLeftToSpawn; 
    private float eps;
    private bool isSpawning = false; 
    private void Awake()     // Método chamado quando o objeto é inicializado.

    {
        onEnemyDestroy.AddListener(EnemyDestroyed); // Adiciona o listener para o evento de destruição de inimigos.
    }
    private void Start()     // Método chamado no início do jogo.

    {
        StartCoroutine(StartWave()); // Inicia a primeira onda de inimigos.
    }
    private void Update()     // Método chamado a cada quadro.

    {
        if (!isSpawning) return;         // Se não estiver gerando inimigos, sai do método.

        timeSinceLastSpawn += Time.deltaTime;        // Atualiza o tempo desde o último inimigo gerado.

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)        // Verifica se é hora de gerar um novo inimigo.

        {
            SpawnEnemy(); // Gera um inimigo.
            enemiesLeftToSpawn--; // Decrementa o número de inimigos restantes a serem gerados.
            enemiesAlive++; // Incrementa o número de inimigos vivos.
            timeSinceLastSpawn = 0f; // Reseta o temporizador.
        }
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)         // Verifica se todos os inimigos foram destruídos.

        {
            EndWave(); // Termina a onda atual.
        }
    }
    private void EnemyDestroyed()     // Método chamado quando um inimigo é destruído.

    {
        enemiesAlive--; // Decrementa o número de inimigos vivos.
    }
    private IEnumerator StartWave()     //   inicia uma nova onda de inimigos.

    {
        yield return new WaitForSeconds(timeBetweenWaves);// Espera pelo tempo entre ondas.
        isSpawning = true; // Marca que os inimigos estão sendo gerados.
        enemiesLeftToSpawn = EnemiesPerWave(); // Calcula quantos inimigos serão gerados nesta onda.
        eps = EnemiesPerSecond(); // Calcula a taxa de geração de inimigos por segundo.
    }
    private void EndWave()     // Método para terminar a onda atual e iniciar uma nova.

    {
        isSpawning = false; // Para a geração de inimigos.
        timeSinceLastSpawn = 0f; // Reseta o temporizador de geração.
        currentWave++; // Avança para a próxima onda.
        StartCoroutine(StartWave()); // Inicia a próxima onda.
    }
    private void SpawnEnemy()     // Método para gerar um inimigo.

    {
        int index = Random.Range(0, enemyPrefabs.Length); // Seleciona um prefab aleatório de inimigo.
        GameObject prefabToSpawn = enemyPrefabs[index]; // Obtém o prefab escolhido.
        Instantiate(prefabToSpawn, LevelManager.instance.startPoint.position, Quaternion.identity); // Instancia o inimigo na posição inicial.
    }
    private int EnemiesPerWave()     // Método para calcular o número de inimigos por onda.

    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor)); // Aplica a escala de dificuldade.
    }
    private float EnemiesPerSecond()     // Método para calcular a taxa de inimigos gerados por segundo.

    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f,enemiesPerSecondCap); // Aplica limite máximo.
    }


}
