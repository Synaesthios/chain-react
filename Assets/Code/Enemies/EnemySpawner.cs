﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class EnemySpawner : MonoBehaviour {

	public Enemy enemy;
	/**
	  This dictionary holds a list of enemy spawn phases. The key "float" represents
	  when an enemySpawnPhase will end.
	 */
	private SortedDictionary<float, EnemySpawnPhase> enemySpawnPhases;
	
	void Start () {
		enemySpawnPhases = new SortedDictionary<float, EnemySpawnPhase>();

		EnemySpawnPhase enemySpawnPhase1 = new EnemySpawnPhase();
		enemySpawnPhase1.minEnemiesSpawned = 3;
		enemySpawnPhase1.maxEnemiesSpawned = 4;
		enemySpawnPhase1.enemiesThatCanSpawn = new List<Enemy>() { enemy };
		enemySpawnPhase1.timeBetweenEnemySpawns = 5;
		enemySpawnPhases[60] = enemySpawnPhase1;
	}
	
	void Update () {
		List<Enemy> enemiesToSpawn = GetCurrentEnemySpawnPhase().GetEnemiesToSpawn(Time.time);
		enemiesToSpawn.ForEach(enemy => SpawnEnemy(enemy));
	}

	private EnemySpawnPhase GetCurrentEnemySpawnPhase() {
		foreach(float timeEnds in enemySpawnPhases.Keys) {
			if (timeEnds < Time.time) {
				return enemySpawnPhases[timeEnds];
			}
		}

		return enemySpawnPhases.Values.Last();
	}
	private void SpawnEnemy(Enemy enemy) {
		Vector3 spawnLocation = GetRandomLevelBackgroundCoordinate(enemy.renderer);
		Instantiate(enemy, spawnLocation, Quaternion.identity);
	}

	private Vector3 GetRandomLevelBackgroundCoordinate(Renderer enemy) {
		float totalXExtent = LevelBounds.bounds.extents.x - enemy.bounds.extents.x;
		float totalZExtent = LevelBounds.bounds.extents.z - enemy.bounds.extents.z;
		
		float randomX = Random.Range (LevelBounds.bounds.center.x - totalXExtent,
		 	LevelBounds.bounds.center.x + totalXExtent);
		
		float randomZ = Random.Range (LevelBounds.bounds.center.z - totalZExtent,
			LevelBounds.bounds.center.z + totalZExtent);
		
		return new Vector3 (randomX, 0, randomZ);
	}
}
