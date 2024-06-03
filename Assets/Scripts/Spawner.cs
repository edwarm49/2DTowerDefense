using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesRate = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficulty = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    public static Spawner main;

    public int currentWave = 1;
    private float timeSinceLastSpawn;
    public int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private void Awake()
    {
        //Wait for something to call this:
        onEnemyDestroy.AddListener(EnemyDestroyed);
        main = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(StartWave());
    }

    
    private IEnumerator StartWave()
    {
        //THis starts a new wave. Each time increasing the speed at which enemies spawn.
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        enemiesRate = enemiesRate + 0.2f;
    }
 
    private int EnemiesPerWave()
    {
        //This increases the amount of enemies spawned per wave.
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficulty));
    }

    private void SpawnEnemy()
    {
        //Choose a random enemy within a list and instantiate it.
        //The more of an enemy there is in the list, the more likely it is to spawn.
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];

        //Don't spawn a Boss Enemy unless it is at least wave 5.
        if (index == 14 && currentWave <= 5)
        {
            return;
        }

        Instantiate(prefabToSpawn, GameManager.main.startPos.position, Quaternion.identity);
    }

    private void Update()
    {
        if (!isSpawning) return;
        //Track how long it's been since an enemy spawned.
        timeSinceLastSpawn += Time.deltaTime;

        //If the time since an enemy has spawned is larger than the spawn rate & there are more enemies to spawn (based on wave difficulty scaler),
        //spawn an enemy and update the wave stats.
        if(timeSinceLastSpawn >= (1f / enemiesRate) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        //Start a new wave when all enemies have been defeated/reached the end.
        if(enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }

        //Band-Aid Fix :(
        CheckForFailedEnemyCount();
    }
    private void EndWave()
    {
        //Turn off spawns and increase wave counter.
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        StartCoroutine(StartWave());
        currentWave++;
    }
    private void EnemyDestroyed()
    {
        //Track how many enemies are left.
        //enemiesAlive--;
    }
    
    private void CheckForFailedEnemyCount()
    {
        //Band-Aid fix for the game miscounting how many enemies are left and soft-locking the wave.
        if (timeSinceLastSpawn > 20f)
        {
            //There may still be enemies alive at this point unfortunately.
            enemiesAlive = 0;
            EndWave();
        }
    }
}
