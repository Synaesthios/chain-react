using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    
    public int Health;

    public List<GameObject> BulletPrefabs;

    protected virtual List<BulletSpawnInfo> BulletPattern { get; set; }
    public bool Alive = true;
    public Renderer renderer;
    [SerializeField]
    private GameObject explosionParticleSystem;

    private bool m_explodeOnNextBeat = false;


    /// <summary>
    /// Called on each enemy type when they run out of health and need to explode.
    /// </summary>
    public abstract void OnExplode();

    private void Start()
    {
        EventSystem.Subscribe<Events.OnBeat>(OnBeat);
    }

    /// <summary>
    /// When colliding with something, maybe explode.
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter(Collider col)
    {
        if (!Alive)
            return;

        if (col.transform.gameObject.tag == "EnemyBullet")
        {
            var enemyBullet = col.gameObject.GetComponent<EnemyBullet>();
            if (enemyBullet != null)
            {
                if (enemyBullet.Owner == gameObject)
                {
                    return;
                }

                enemyBullet.Die();
            }
            else
            {
                Destroy(col.gameObject);
            }
        }
        else if (col.transform.gameObject.tag == "PlayerBullet")
        {
            var playerBullet = col.gameObject.GetComponent<PlayerBullet>();
            if (playerBullet != null)
                playerBullet.HitEnemy();
        }

        Health -= 1;
        if(Health <= 0)
        {
            // Functionally die
            Die();
            m_explodeOnNextBeat = true;
        }
    }

    /// <summary>
    /// Called when a beat happens. Enemies only explode on the next beat after they die.
    /// </summary>
    private void OnBeat(Events.OnBeat evt)
    {
        if (m_explodeOnNextBeat)
        {
            EventSystem.Unsubscribe<Events.OnBeat>(OnBeat);
            Explode();
        }
    }

    /// <summary>
    /// Explode, destroy myself and fire some stuff
    /// </summary>
    private void Explode()
    {
        Camera.main.GetComponent<CameraShake>().PlayShake();
        explosionParticleSystem.SetActive(true);
        // Call the child override function.
        OnExplode();

        // Fire bullets
        FireBulletPattern();

        Destroy(gameObject, 3f);
        var renderer = GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            //renderer.material.color = new Color(1f, 1f, 1f, .3f) * renderer.material.color;
            renderer.enabled = false;
        }

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
    protected void FireBulletPattern(List<BulletSpawnInfo> bulletPattern = null)
    {
        // If nothing is provided, use the member variable.
        if(bulletPattern == null)
        {
            bulletPattern = BulletPattern;
        }

        // Fire all bullets in the pattern
        foreach (var bulletInfo in bulletPattern)
        {
            FireBullet(bulletInfo);
        }
    }

    /// <summary>
    /// Fire the bullet.
    /// </summary>
    /// <param name="bullet"></param>
    public void FireBullet(BulletSpawnInfo bulletInfo)
    {
        StartCoroutine(FireBulletWithDelay(bulletInfo));
    }

    /// <summary>
    /// Fire them with a delay.
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    private IEnumerator FireBulletWithDelay(BulletSpawnInfo info)
    {
        yield return new WaitForSeconds(info.Delay);

        var prefab = BulletPrefabs[info.PrefabIndex];
        var bulletObject = ObjectPool.Instance.Acquire(prefab, transform.position + info.StartOffset, Quaternion.identity);

        EnemyBullet bullet = bulletObject.GetComponent<EnemyBullet>();
        bullet.Initialize(info, gameObject, () => { ObjectPool.Instance.Release(prefab, bulletObject); });
    }
}
