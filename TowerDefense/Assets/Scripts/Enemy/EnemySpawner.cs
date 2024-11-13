// Importa as bibliotecas necessárias
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs; // Lista de prefabs de inimigos a serem instanciados.
    [SerializeField] private float enemiesPerSecond = 0.5f; // Taxa inicial de inimigos por segundo.
    [SerializeField] private float timeBetweenWaves = 5f; // Intervalo entre ondas de inimigos.
    [SerializeField] private float difficultyScalingFactor = 0.75f; // Fator de escala para dificuldade.
    [SerializeField] private float enemiesPerSecondCap = 15f; // Limite máximo de inimigos por segundo.

    public static UnityEvent onEnemyDestroy = new UnityEvent(); // Evento estático disparado quando um inimigo é destruído.

    private int currentWave = 1; // Contador da onda atual.
    private float timeSinceLastSpawn; // Tempo desde o último inimigo gerado.
    private int enemiesAlive; // Contador de inimigos vivos.
    private int enemiesLeftToSpawn; // Contador de inimigos restantes para spawn na onda atual.
    private float eps; // Variável que armazena a taxa de spawn ajustada.
    private bool isSpawning = false; // Indica se o spawn está ativo.

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed); // Adiciona um listener para o evento de destruição de inimigos.
    }

    private void Start()
    {
        StartCoroutine(StartWave()); // Inicia a primeira onda.
    }

    private void Update()
    {
        if (!isSpawning) return; // Verifica se o spawn está ativo.

        timeSinceLastSpawn += Time.deltaTime; // Incrementa o tempo desde o último spawn.

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0) // Verifica se é hora de spawnar outro inimigo.
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0) // Checa se todos os inimigos foram derrotados.
        {
            EndWave(); // Finaliza a onda.
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--; // Diminui o contador de inimigos vivos.
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves); // Espera o intervalo entre ondas.
        isSpawning = true; // Ativa o spawn de inimigos.
        enemiesLeftToSpawn = EnemiesPerWave(); // Define o número de inimigos para a onda.
        eps = EnemiesPerSecond(); // Define a taxa de spawn.
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++; // Incrementa a onda atual.
        StartCoroutine(StartWave()); // Inicia a próxima onda.
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Count); // Seleciona um prefab aleatório.
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.instance.startPoint.position, Quaternion.identity); // Instancia o inimigo.
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor)); // Calcula o número de inimigos com base na dificuldade.
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap); // Calcula a taxa de spawn.
    }
}
