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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            FireBulletPattern();
        }
    }

    /// <summary>
    /// Create bullets in all compass directions (8 pointed starburst)
    /// </summary>
    public override void OnExplode()
    {

    }
}
