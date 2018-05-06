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

    /// <summary>
    /// When the  bullet is created by an enemy
    /// </summary>
    /// <param name="movementVector"></param>
	public void Initialize(BulletSpawnInfo spawnInfo, GameObject owner)
    {
        m_info = spawnInfo;
        Owner = owner;

        if (m_info.Lifetime > 0)
        {
            Destroy(gameObject, m_info.Lifetime);
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
}
