using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
  Describes how enemies will spawn during a phase
 */
public class EnemySpawnPhase {
    public int maxEnemiesSpawned;
    public int minEnemiesSpawned;
    public List<Enemy> enemiesThatCanSpawn;
    public float timeBetweenEnemySpawns;

    private float timeSinceLastRespawn;

    /**
        Gets a list of enemies to spawn depending on the current time in the game
     */
    public List<Enemy> GetEnemiesToSpawn(float currentTime) {
        List<Enemy> enemiesToSpawn = new List<Enemy>();
        if (currentTime > timeBetweenEnemySpawns + timeSinceLastRespawn) {
            float numberOfEnemies = Random.Range(minEnemiesSpawned, maxEnemiesSpawned);
            for (int i = 0; i < numberOfEnemies; i++) {
                enemiesToSpawn.Add(
                    enemiesThatCanSpawn[Random.Range(0, enemiesThatCanSpawn.Count-1)]);
            }
            timeSinceLastRespawn = currentTime;
        }

        return enemiesToSpawn;
    }


}