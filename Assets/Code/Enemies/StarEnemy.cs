using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarEnemy : Enemy
{
    private const float BulletMoveSpeed = 4f;


    /// <summary>
    /// The directions of all of the fired bullets.
    /// </summary>
    protected override List<BulletSpawnInfo> BulletPattern
    {
        get
        {
            return new List<BulletSpawnInfo>()
            {
                new BulletSpawnInfo()
                {
                    Delay = 0,
                    Direction = new Vector3(1, 0, 0),
                    Speed = BulletMoveSpeed,
                    PrefabIndex = 0
                },
                new BulletSpawnInfo()
                {
                    Delay = .5f,
                    Direction = (new Vector3(1, 0, 1)).normalized,
                    Speed = BulletMoveSpeed,
                    PrefabIndex = 0
                },
                new BulletSpawnInfo()
                {
                    Delay = 0,
                    Direction = new Vector3(0, 0, 1),
                    Speed = BulletMoveSpeed,
                    PrefabIndex = 0
                },
                new BulletSpawnInfo()
                {
                    Delay = .5f,
                    Direction = new Vector3(-1, 0, 1).normalized,
                    Speed = BulletMoveSpeed,
                    PrefabIndex = 0
                },
                new BulletSpawnInfo()
                {
                    Delay = 0,
                    Direction = new Vector3(-1, 0, 0),
                    Speed = BulletMoveSpeed,
                    PrefabIndex = 0
                },
                new BulletSpawnInfo()
                {
                    Delay = .5f,
                    Direction = new Vector3(-1, 0, -1).normalized,
                    Speed = BulletMoveSpeed,
                    PrefabIndex = 0
                },
                new BulletSpawnInfo()
                {
                    Delay = 0,
                    Direction = new Vector3(0, 0, -1),
                    Speed = BulletMoveSpeed,
                    PrefabIndex = 0
                },
                new BulletSpawnInfo()
                {
                    Delay = .5f,
                    Direction = new Vector3(1, 0, -1).normalized,
                    Speed = BulletMoveSpeed,
                    PrefabIndex = 0
                }
            };
        }
    }

    /// <summary>
    /// Create bullets in all compass directions (8 pointed starburst)
    /// </summary>
    public override void OnExplode()
    {

    }
}
