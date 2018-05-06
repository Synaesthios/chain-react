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
    public Enemy Boss;
    public float Duration;

    private float timeSinceLastRespawn;

    // 120 bpm to 4x spawn multiplier with 0.03333f
    private const float c_spawnMultiplierPerBeat = 0.0333333f; 

    /**
        Gets a list of enemies to spawn depending on the current time in the game
     */
    public List<Enemy> GetEnemiesToSpawn(float currentTime) {
        List<Enemy> enemiesToSpawn = new List<Enemy>();
        if (currentTime > timeBetweenEnemySpawns + timeSinceLastRespawn) {
            float numberOfEnemies = Random.Range(minEnemiesSpawned, maxEnemiesSpawned);
            numberOfEnemies = Mathf.RoundToInt(numberOfEnemies * MusicManager.CurrentBPM * c_spawnMultiplierPerBeat);
            for (int i = 0; i < numberOfEnemies; i++) {
                enemiesToSpawn.Add(
                    enemiesThatCanSpawn[Random.Range(0, enemiesThatCanSpawn.Count)]);
            }
            timeSinceLastRespawn = currentTime;
        }

        return enemiesToSpawn;
    }


}