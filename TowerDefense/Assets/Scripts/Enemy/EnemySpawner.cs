using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private GameObject[] enemyPrefabs;��� // Prefabs dos inimigos que podem ser gerados.
 �� [SerializeField] private int baseEnemies = 8;��������� // N�mero base de inimigos por onda.
 �� [SerializeField] private float spawnRate = 0.5f;������ // N�mero de inimigos gerados por segundo.
 �� [SerializeField] private float waveInterval = 5f;����� // Tempo entre ondas de inimigos.
 �� [SerializeField] private float difficultyScale = 0.75f; // Fator de escalonamento de dificuldade por onda.
 �� [SerializeField] private float maxSpawnRate = 15f;���� // Limite m�ximo de inimigos gerados por segundo.



 �� public static UnityEvent onEnemyDestroyed = new UnityEvent(); // Evento disparado quando um inimigo � destru�do.



 �� private int waveNumber = 1;�������������� // N�mero da onda atual.
 �� private float spawnTimer = 0f;����������� // Tempo desde o �ltimo inimigo gerado.
 �� private int remainingEnemies;������������ // Inimigos restantes a serem gerados na onda atual.
 �� private int aliveEnemies = 0;������������ // N�mero de inimigos vivos.
 �� private bool spawning = false;����������� // Indica se a onda de inimigos est� em andamento.
 �� private float currentSpawnRate;           // Taxa de gera��o de inimigos ajustada para a onda atual.


    private void Awake()
    {
        onEnemyDestroyed.AddListener(HandleEnemyDestroyed); // Executa o evento de destrui��o de inimigos.
 �� }



    private void Start()
    {
        StartCoroutine(InitializeWave()); // Inicia a primeira onda.
 �� }


    private void Update()
    {
        if (!spawning) return;



        spawnTimer += Time.deltaTime;



        if (spawnTimer >= (1f / currentSpawnRate) && remainingEnemies > 0)
        {
            SpawnEnemy();
            remainingEnemies--;
            aliveEnemies++;
            spawnTimer = 0f;
        }



        if (aliveEnemies == 0 && remainingEnemies == 0)
        {
            CompleteWave();
        }
    }



    private void HandleEnemyDestroyed()
    {
        aliveEnemies--;
    }



    private IEnumerator InitializeWave()
    {
        yield return new WaitForSeconds(waveInterval);
        spawning = true;
        remainingEnemies = CalculateEnemiesPerWave();
        currentSpawnRate = CalculateSpawnRate();
    }



    private void CompleteWave()
    {
        spawning = false;
        spawnTimer = 0f;
        waveNumber++;
        StartCoroutine(InitializeWave());
    }



    private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyToSpawn = enemyPrefabs[randomIndex];
        Instantiate(enemyToSpawn, LevelManager.instance.startPoint.position, Quaternion.identity);
    }



    private int CalculateEnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(waveNumber, difficultyScale));
    }



    private float CalculateSpawnRate()
    {
        return Mathf.Clamp(spawnRate * Mathf.Pow(waveNumber, difficultyScale), 0f, maxSpawnRate);
    }
}
