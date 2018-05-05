using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BulletSpawnInfo
{
    public float Delay;
    public Vector3 Direction;
    public float Speed;
    public int PrefabIndex;
}

public class EnemyBullet : MonoBehaviour
{
    BulletSpawnInfo m_info;
    
    /// <summary>
    /// When the  bullet is created by an enemy
    /// </summary>
    /// <param name="movementVector"></param>
	public void Initialize(BulletSpawnInfo spawnInfo)
    {
        m_info = spawnInfo;
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
}
