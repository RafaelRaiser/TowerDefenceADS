using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    // Lista de prefabs de inimigos dispon�veis para spawnar
    [SerializeField] private List<GameObject> enemyPrefabs;

    // Taxa base de spawn de inimigos por segundo
    [SerializeField] private float enemiesPerSecond = 0.5f;

    // Tempo entre ondas de inimigos
    [SerializeField] private float timeBetweenWaves = 5f;

    // Fator de escalonamento para a dificuldade (quanto maior, mais dif�cil)
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    // Limite m�ximo de inimigos por segundo para evitar spawns excessivos
    [SerializeField] private float enemiesPerSecondCap = 15f;

    // Evento que � disparado quando um inimigo � destru�do
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    // Contador de ondas, come�a com 1
    private int currentWave = 1;

    // Tempo acumulado desde o �ltimo spawn
    private float timeSinceLastSpawn;

    // Contador de inimigos vivos
    private int enemiesAlive;

    // N�mero de inimigos restantes para spawnar nesta onda
    private int enemiesLeftToSpawn;

    // Taxa de spawn calculada para a onda atual
    private float eps;

    // Estado indicando se o spawn de inimigos est� ativo
    private bool isSpawning = false;

    private void Awake()
    {
        // Adiciona o m�todo EnemyDestroyed ao evento de destrui��o de inimigos
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        // Inicia a primeira onda de inimigos ao iniciar o jogo
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        // Se n�o est� no processo de spawn, retorna
        if (!isSpawning) return;

        // Incrementa o tempo desde o �ltimo spawn
        timeSinceLastSpawn += Time.deltaTime;

        // Verifica se � hora de spawnar um novo inimigo com base em eps (inimigos por segundo)
        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            // Spawn de um inimigo e ajuste de contadores
            SpawnEnemy();
            enemiesLeftToSpawn--; // Reduz o n�mero de inimigos a spawnar nesta onda
            enemiesAlive++; // Incrementa o n�mero de inimigos vivos
            timeSinceLastSpawn = 0f; // Reseta o tempo desde o �ltimo spawn
        }

        // Verifica se todos os inimigos foram spawnados e destru�dos
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            // Termina a onda atual e inicia a pr�xima
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        // Diminui o n�mero de inimigos vivos ao ser notificado que um inimigo foi destru�do
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        // Espera o tempo entre ondas antes de come�ar a nova onda
        yield return new WaitForSeconds(timeBetweenWaves);

        // Inicia o spawn da nova onda
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave(); // Calcula o n�mero de inimigos a spawnar
        eps = EnemiesPerSecond(); // Define a taxa de spawn ajustada para a onda atual
    }

    private void EndWave()
    {
        // Finaliza a onda atual e prepara para a pr�xima
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++; // Incrementa o contador de ondas
        StartCoroutine(StartWave()); // Inicia a pr�xima onda
    }

    private void SpawnEnemy()
    {
        // Seleciona um prefab de inimigo aleat�rio da lista para spawnar
        int index = Random.Range(0, enemyPrefabs.Count);
        GameObject prefabToSpawn = enemyPrefabs[index];

        // Instancia o inimigo no ponto de spawn inicial do LevelManager
        Instantiate(prefabToSpawn, LevelManager.instance.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        // Calcula o n�mero de inimigos na onda atual usando um fator de escalonamento de dificuldade
        return Mathf.RoundToInt(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private float EnemiesPerSecond()
    {
        // Calcula a taxa de spawn para a onda atual, respeitando o limite m�ximo
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }
}
