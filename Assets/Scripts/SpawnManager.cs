using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    float delayEnemySpawn = 2f;
    float minDelayEnemySpawn = 0.25f;
    float nextEnemySpawn = 0f;
    float delayPowerupSpawn = 5f;
    float nextPowerupSpawn = 10f;
    public bool spawnActive = true;

    [SerializeField]
    GameObject playerPrefab;

    //Enemies
    [SerializeField]
    GameObject catPrefab;
    [SerializeField]
    GameObject fireworksPrefab;
    [SerializeField]
    GameObject mailManPrefab;

    //Power-ups
    [SerializeField]
    GameObject bonePrefab;
    [SerializeField]
    GameObject tennisBallPrefab;
    [SerializeField]
    GameObject kibblePrefab;

    Timer timer;

    List<GameObject> enemyPrefabs;
    List<GameObject> powerupPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("Timer").GetComponent<Timer>();

        //spawns player
        Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //list of enemies
        enemyPrefabs = new List<GameObject>();
        enemyPrefabs.Add(catPrefab);
        enemyPrefabs.Add(fireworksPrefab);
        enemyPrefabs.Add(mailManPrefab);

        //list of power-ups
        powerupPrefabs = new List<GameObject>();
        powerupPrefabs.Add(bonePrefab);
        powerupPrefabs.Add(tennisBallPrefab);
        powerupPrefabs.Add(kibblePrefab);

        //spawn variables
        float randomX = Random.Range(-9f, 9f);
        Vector2 spawnPos = new Vector2(randomX, 6f);
        int randomEnemy = Random.Range(0, enemyPrefabs.Count);
        int randomPowerup = Random.Range(0, powerupPrefabs.Count);
        
        //spawns enemy every 2 seconds
        if (Time.time >= nextEnemySpawn && spawnActive)
        {
            nextEnemySpawn = Time.time + delayEnemySpawn;
            Instantiate(enemyPrefabs[randomEnemy], spawnPos, Quaternion.identity);

            /*
            if (delayEnemySpawn > minDelayEnemySpawn)
            {
                //delayEnemySpawn -= 0.01f;
                for(int i = 0; i >= timer.min; i++)
                {
                    delayEnemySpawn -= 0.05f;
                }
            }
            */
        }
        //spawns power-up every 5 seconds
        if (Time.time >= nextPowerupSpawn && spawnActive)
        {
            nextPowerupSpawn = Time.time + delayPowerupSpawn;
            Instantiate(powerupPrefabs[randomPowerup], spawnPos, Quaternion.identity);
        }
    }
}
