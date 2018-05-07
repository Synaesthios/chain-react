﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class EnemySpawner : MonoBehaviour {
    public Renderer levelBackground;

    [SerializeField]
	private List<EnemySpawnPhase> SpawnPhases;

    private float m_timeSinceLastPhase = 0;
    private int m_currentPhase = 0;

	/**
	  This dictionary holds a list of enemy spawn phases. The key "float" represents
	  when an enemySpawnPhase will end.
	 */
	
	void Start () {
        /*
        SpawnPhases = new List<EnemySpawnPhase>();

        // Enemies 1
        SpawnPhases.Add(new EnemySpawnPhase()
        {
            Duration = 30,
            minEnemiesSpawned = 3,
            maxEnemiesSpawned = 4,
            enemiesThatCanSpawn = new List<Enemy>() { basicEnemy, followerEnemy },
            timeBetweenEnemySpawns = 5
        });

        // Boss 1
        SpawnPhases.Add(new EnemySpawnPhase()
        {
            Boss = circleBoss,
            minEnemiesSpawned = 3,
            maxEnemiesSpawned = 4,
            enemiesThatCanSpawn = new List<Enemy>() { basicEnemy, followerEnemy },
            timeBetweenEnemySpawns = 5
        });

        // Enemies 2
        SpawnPhases.Add(new EnemySpawnPhase()
        {
            Duration = 30,
            minEnemiesSpawned = 4,
            maxEnemiesSpawned = 5,
            enemiesThatCanSpawn = new List<Enemy>() { basicEnemy, followerEnemy, followerEnemy },
            timeBetweenEnemySpawns = 4
        });

        // Boss 2
        SpawnPhases.Add(new EnemySpawnPhase()
        {
            Boss = squareBoss,
            minEnemiesSpawned = 3,
            maxEnemiesSpawned = 4,
            enemiesThatCanSpawn = new List<Enemy>() { basicEnemy, followerEnemy },
            timeBetweenEnemySpawns = 5
        });

        // Enemies 3
        SpawnPhases.Add(new EnemySpawnPhase()
        {
            Duration = 60,
            minEnemiesSpawned = 5,
            maxEnemiesSpawned = 6,
            enemiesThatCanSpawn = new List<Enemy>() { basicEnemy, followerEnemy, followerEnemy },
            timeBetweenEnemySpawns = 5
        });

        // Boss 3
        SpawnPhases.Add(new EnemySpawnPhase()
        {
            Boss = circleBoss,
            minEnemiesSpawned = 4,
            maxEnemiesSpawned = 5,
            enemiesThatCanSpawn = new List<Enemy>() { followerEnemy, followerEnemy },
            timeBetweenEnemySpawns = 5
        });

        // Enemies 4
        SpawnPhases.Add(new EnemySpawnPhase()
        {
            Duration = 60,
            minEnemiesSpawned = 5,
            maxEnemiesSpawned = 6,
            enemiesThatCanSpawn = new List<Enemy>() { basicEnemy, followerEnemy, followerEnemy },
            timeBetweenEnemySpawns = 5
        });

        // Boss 4
        SpawnPhases.Add(new EnemySpawnPhase()
        {
            Boss = squareBoss,
            minEnemiesSpawned = 5,
            maxEnemiesSpawned = 6,
            enemiesThatCanSpawn = new List<Enemy>() { followerEnemy, followerEnemy },
            timeBetweenEnemySpawns = 5
        });
        */

        EventSystem.Subscribe<Events.BossDied>(OnBossDied);
    }
	
	void Update () {
        var player = GameObject.FindObjectOfType<PlayerScript>();
        if (player == null || player.isDead())
        {
            return;
        }

        EnemySpawnPhase currentPhase = GetCurrentEnemySpawnPhase();

        List<Enemy> enemiesToSpawn = currentPhase.GetEnemiesToSpawn(m_timeSinceLastPhase);
		enemiesToSpawn.ForEach(enemy => SpawnEnemy(enemy));

        m_timeSinceLastPhase += Time.deltaTime;

        if(m_timeSinceLastPhase > currentPhase.Duration && currentPhase.Duration > 0)
        {
            StartNextPhase();
        }

    }

    private void OnBossDied(Events.BossDied evt)
    {
        StartNextPhase();
    }

    private void StartNextPhase()
    {
        // Reset the counters and indexes
        m_timeSinceLastPhase = 0;
        m_currentPhase++;
        if (m_currentPhase >= SpawnPhases.Count)
        {
            // Don't loop back to easy mode, loop through the harder modes.
            m_currentPhase = 4;
        }

        // Spawn a boss if one is needed
        EnemySpawnPhase newPhase = GetCurrentEnemySpawnPhase();
        if(newPhase.Boss != null)
        {
            GameObject.Instantiate(newPhase.Boss, GetRandomLevelBackgroundCoordinate(newPhase.Boss.GetComponent<Renderer>()), Quaternion.identity);
        }
    }

	private EnemySpawnPhase GetCurrentEnemySpawnPhase() {
        return SpawnPhases[m_currentPhase];
	}
	private void SpawnEnemy(Enemy enemy) {
		Vector3 spawnLocation = GetRandomLevelBackgroundCoordinate(enemy.renderer);
		Instantiate(enemy, spawnLocation, Quaternion.identity);
	}

	private Vector3 GetRandomLevelBackgroundCoordinate(Renderer enemy) {
		float totalXExtent = LevelBounds.bounds.extents.x - enemy.bounds.extents.x;
		float totalZExtent = LevelBounds.bounds.extents.z - enemy.bounds.extents.z;

        PlayerScript player = GameObject.FindObjectOfType<PlayerScript>();

        Vector3 potentialPosition;
        do
        {
            float randomX = Random.Range(levelBackground.bounds.center.x - totalXExtent,
             levelBackground.bounds.center.x + totalXExtent);

            float randomZ = Random.Range(levelBackground.bounds.center.z - totalZExtent,
                levelBackground.bounds.center.z + totalZExtent);

            potentialPosition = new Vector3(randomX, 0, randomZ);
        }
        while (player != null && Vector3.Distance(potentialPosition, player.transform.position) < 8);
        
        return potentialPosition;
	}
}
