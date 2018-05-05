using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    
    public int Health;

    public List<GameObject> BulletPrefabs;

    protected virtual List<BulletSpawnInfo> BulletPattern { get; set; }
    protected bool Alive = true;
    public Renderer renderer;

    /// <summary>
    /// Called on each enemy type when they run out of health and need to explode.
    /// </summary>
    public abstract void OnExplode();

    /// <summary>
    /// When colliding with something, maybe explode.
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter(Collider col)
    {
        if(col.transform.gameObject.tag == "EnemyBullet" || col.transform.gameObject.tag == "PlayerBullet")
        {
            Explode();
            Destroy(col.gameObject);
        }
    }

    /// <summary>
    /// Explode, destroy myself and fire some stuff
    /// </summary>
    private void Explode()
    {
        // Call the child override function.
        OnExplode();

        // Fire bullets
        FireBulletPattern();

        // Functionally die
        Die();

        // Destroy myself.
        Destroy(gameObject, 3f);
    }

    /// <summary>
    /// Act dead, even if the game object needs to still be alive to spawn bullets.
    /// </summary>
    private void Die()
    {
        GetComponent<Collider>().enabled = false;
        EventSystem.Fire(new Events.EnemyDied(1));
        Alive = false;
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
