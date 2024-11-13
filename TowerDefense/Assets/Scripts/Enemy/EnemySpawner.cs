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
    // N�mero de inimigos gerados por segundo
    [SerializeField] private float enemiesPerSecond = 0.5f;
    // Tempo de espera entre as ondas de inimigos
    [SerializeField] private float timeBetweenWaves = 5f;
    // Fator que aumenta a dificuldade a cada onda
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    // Limite de inimigos por segundo para evitar excessos
    [SerializeField] private float enemiesPerSecondCap = 15f;

    // Evento est�tico chamado quando um inimigo � destru�do
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    // Vari�veis internas para controle das ondas e inimigos vivos
    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps;
    private bool isSpawning = false;

    // M�todo chamado quando o objeto � instanciado
    private void Awake()
    {
        // Adiciona um ouvinte para o evento de destrui��o de inimigos
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    // Inicia a primeira onda de inimigos
    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        // Checa se � para gerar inimigos
        if (!isSpawning) return;

        // Incrementa o tempo desde o �ltimo spawn
        timeSinceLastSpawn += Time.deltaTime;

        // Checa se � hora de gerar um novo inimigo
        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
        // Se todos os inimigos foram gerados e destru�dos, termina a onda
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    // M�todo chamado quando um inimigo � destru�do, reduz o contador de vivos
    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    // Inicia a nova onda de inimigos ap�s esperar o tempo necess�rio
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }

    // Termina a onda atual e prepara a pr�xima
    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    // M�todo para instanciar um inimigo aleat�rio na posi��o inicial
    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Count); // Seleciona aleatoriamente um prefab de inimigo
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.instance.startPoint.position, Quaternion.identity);
    }

    // Calcula o n�mero de inimigos na onda com base no fator de dificuldade
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    // Calcula a taxa de gera��o de inimigos com base no fator de dificuldade, limitado ao cap
    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }
}
