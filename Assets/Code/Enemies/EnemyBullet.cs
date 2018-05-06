using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BulletSpawnInfo
{
    public float Delay;
    public Vector3 Direction;
    public float Speed;
    public float Lifetime;
    public int PrefabIndex;
    public Vector3 StartOffset;
}

public class EnemyBullet : MonoBehaviour
{
    BulletSpawnInfo m_info;
    public GameObject Owner { get; set; }
    private System.Action m_onDeath;

    /// <summary>
    /// When the  bullet is created by an enemy
    /// </summary>
    /// <param name="movementVector"></param>
	public void Initialize(BulletSpawnInfo spawnInfo, GameObject owner, System.Action onDeath)
    {
        m_info = spawnInfo;
        Owner = owner;
        m_onDeath = onDeath;
        gameObject.SetActive(true);

        if (m_info.Lifetime > 0)
        {
            Invoke("Die", m_info.Lifetime);
        }
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        transform.position += m_info.Direction * m_info.Speed * Time.deltaTime;
    }

    private void OnValidate()
    {
        gameObject.tag = "EnemyBullet";
        gameObject.layer = LayerMask.NameToLayer("EnemyBullets");
    }

    public void Die()
    {
        gameObject.SetActive(false);
        CancelInvoke();
        m_onDeath();
    }
}
