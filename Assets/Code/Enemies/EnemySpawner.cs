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
        EventSystem.Subscribe<Events.PlayerDied>(OnPlayerDied);
        EventSystem.Subscribe<Events.BossDied>(OnBossDied);
        GetCurrentEnemySpawnPhase().Setup();
    }

    private void OnDestroy()
    {
        EventSystem.Unsubscribe<Events.PlayerDied>(OnPlayerDied);
        EventSystem.Unsubscribe<Events.BossDied>(OnBossDied);
    }

    private void OnPlayerDied(Events.PlayerDied evt)
    {
        enabled = false;
    }

    void Update () {
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
        GetCurrentEnemySpawnPhase().Setup();
        if (m_currentPhase >= SpawnPhases.Count)
        {
            // Don't loop back to easy mode, loop through the harder modes.
            m_currentPhase = 4;
        }

        // Spawn a boss if one is needed
        EnemySpawnPhase newPhase = GetCurrentEnemySpawnPhase();
        if(newPhase.Boss != null)
        {
            GameObject.Instantiate(newPhase.Boss, GetRandomLevelBackgroundCoordinate(newPhase.Boss.GetComponentInChildren<Renderer>()), Quaternion.identity);
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
