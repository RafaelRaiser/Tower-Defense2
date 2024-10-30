using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyTemplates;
    [SerializeField] private int initialEnemyCount = 10;
    [SerializeField] private float spawnRate = 0.5f;
    [SerializeField] private float intervalBetweenWaves = 5f;
    [SerializeField] private float difficultyFactor = 0.75f;
    [SerializeField] private float spawnRateCap = 15f;

    public static UnityEvent onEnemyRemoved = new UnityEvent();

    private int waveNumber = 1;
    private float spawnTimer;
    private int activeEnemies;
    private int remainingEnemiesToSpawn;
    private float enemiesPerSecond;
    private bool spawningActive = false;

    private void Awake()
    {
        onEnemyRemoved.AddListener(RemoveEnemy); // Conecta a função para ser chamada ao remover um inimigo
    }

    private void Start()
    {
        StartCoroutine(InitializeWave()); // Começa a primeira onda de inimigos.
    }

    private void Update()
    {
        if (!spawningActive) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= (1f / enemiesPerSecond) && remainingEnemiesToSpawn > 0)
        {
            CreateEnemy();
            remainingEnemiesToSpawn--;
            activeEnemies++;
            spawnTimer = 0f;
        }

        if (activeEnemies == 0 && remainingEnemiesToSpawn == 0)
        {
            CompleteWave();
        }
    }

    private void RemoveEnemy()
    {
        activeEnemies--; // Decrementa o número de inimigos ativos na cena.
    }

    private IEnumerator InitializeWave()
    {
        yield return new WaitForSeconds(intervalBetweenWaves);
        spawningActive = true;
        remainingEnemiesToSpawn = CalculateEnemiesPerWave();
        enemiesPerSecond = CalculateEnemiesPerSecond();
    }

    private void CompleteWave()
    {
        spawningActive = false;
        spawnTimer = 0f;
        waveNumber++;
        StartCoroutine(InitializeWave());
    }

    private void CreateEnemy()
    {
        int index = Random.Range(0, enemyTemplates.Length);
        GameObject selectedTemplate = enemyTemplates[index];
        Instantiate(selectedTemplate, LevelManager.instance.startPoint.position, Quaternion.identity);
    }

    private int CalculateEnemiesPerWave()
    {
        return Mathf.RoundToInt(initialEnemyCount * Mathf.Pow(waveNumber, difficultyFactor));
    }

    private float CalculateEnemiesPerSecond()
    {
        return Mathf.Clamp(spawnRate * Mathf.Pow(waveNumber, difficultyFactor), 0f, spawnRateCap);
    }
}