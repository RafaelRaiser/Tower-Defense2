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
        onEnemyDestroyed.AddListener(HandleEnemyDestroyed); // Escuta o evento de destrui��o de inimigos.
 �� }



    private void Start()
    {
        StartCoroutine(InitializeWave()); // Inicia a primeira onda.
 �� }
}
