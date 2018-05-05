using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    
    public int Health;

    public List<GameObject> BulletPrefabs;

    protected virtual List<BulletSpawnInfo> BulletPattern { get; set; }

    /// <summary>
    /// Called on each enemy type when they run out of health and need to explode.
    /// </summary>
    public abstract void OnExplode();

    /// <summary>
    /// When colliding with something, maybe explode.
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision)
    {
        
    }

    /// <summary>
    /// Explode, destroy myself and fire some stuff
    /// </summary>
    private void Explode()
    {
        // Call the child override function.
        OnExplode();

        FireBulletPattern();

        // Destroy myself.
        Destroy(gameObject, 3f);
    }

    /// <summary>
    /// Fire bullets in a pattern.
    /// </summary>
    protected void FireBulletPattern()
    {
        // Fire all bullets in the pattern
        foreach (var bulletInfo in BulletPattern)
        {
            StartCoroutine(FireBulletWithDelay(bulletInfo));
        }
    }

    /// <summary>
    /// Fire them with a delay.
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    private IEnumerator FireBulletWithDelay(BulletSpawnInfo info)
    {
        yield return new WaitForSeconds(info.Delay);

        var bulletObject = GameObject.Instantiate(BulletPrefabs[info.PrefabIndex], transform.position, Quaternion.identity) as GameObject;
        EnemyBullet bullet = bulletObject.GetComponent<EnemyBullet>();
        bullet.Initialize(info);
    }
}
