using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemySpawner : MonoBehaviour
{
    // Lista dos inimigos que podem ser gerados
    [SerializeField] private List<GameObject> enemyPrefabs;
    // Número de inimigos gerados por segundo
    [SerializeField] private float enemiesPerSecond = 0.5f;
    // Tempo de espera entre as ondas de inimigos
    [SerializeField] private float timeBetweenWaves = 5f;
    // Fator que aumenta a dificuldade a cada onda
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    // Limite de inimigos por segundo para evitar excessos
    [SerializeField] private float enemiesPerSecondCap = 15f;

    // Evento estático chamado quando um inimigo é destruído
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    // Variáveis internas para controle das ondas e inimigos vivos
    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps;
    private bool isSpawning = false;

    // Método chamado quando o objeto é instanciado
    private void Awake()
    {
        // Adiciona um ouvinte para o evento de destruição de inimigos
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    // Inicia a primeira onda de inimigos
    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        // Checa se é para gerar inimigos
        if (!isSpawning) return;

        // Incrementa o tempo desde o último spawn
        timeSinceLastSpawn += Time.deltaTime;

        // Checa se é hora de gerar um novo inimigo
        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
        // Se todos os inimigos foram gerados e destruídos, termina a onda
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    // Método chamado quando um inimigo é destruído, reduz o contador de vivos
    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    // Inicia a nova onda de inimigos após esperar o tempo necessário
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }

    // Termina a onda atual e prepara a próxima
    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    // Método para instanciar um inimigo aleatório na posição inicial
    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Count); // Seleciona aleatoriamente um prefab de inimigo
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.instance.startPoint.position, Quaternion.identity);
    }

    // Calcula o número de inimigos na onda com base no fator de dificuldade
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    // Calcula a taxa de geração de inimigos com base no fator de dificuldade, limitado ao cap
    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }
}
