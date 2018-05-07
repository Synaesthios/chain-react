using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
  Describes how enemies will spawn during a phase
 */
 [CreateAssetMenu(fileName = "EnemySpawnPhase", menuName = "Game/EnemySpawnPhase")]
public class EnemySpawnPhase : ScriptableObject
{
    public int maxEnemiesSpawned;
    public int minEnemiesSpawned;
    public List<GameObject> enemiesThatCanSpawn = new List<GameObject>();
    public float timeBetweenEnemySpawns;
    public GameObject Boss;
    public float Duration;

    private float timeSinceLastRespawn = 0;

    // 120 bpm to 4x spawn multiplier with 0.03333f
    private const float c_spawnMultiplierPerBeat = 0.0333333f;

    private void OnEnable()
    {
        timeSinceLastRespawn = 0;
    }
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
                    enemiesThatCanSpawn[Random.Range(0, enemiesThatCanSpawn.Count)].GetComponent<Enemy>());
            }
            timeSinceLastRespawn = currentTime;
        }

        return enemiesToSpawn;
    }

    private void OnValidate()
    {
        for (int i = enemiesThatCanSpawn.Count - 1; i >= 0; i--)
        {
            if (enemiesThatCanSpawn[i] != null && enemiesThatCanSpawn[i].GetComponent<Enemy>() == null)
            {
                Debug.LogWarning("Invalid enemy game object");
                enemiesThatCanSpawn[i] = null;
            }
        }

        if (Boss != null && Boss.GetComponent<Enemy>() == null)
        {
            Debug.LogWarning("Invalid boss object");
            Boss = null;
        }
    }
}