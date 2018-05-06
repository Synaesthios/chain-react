using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemy : Enemy
{
    /// <summary>
    /// The directions of all of the fired bullets.
    /// </summary>
    protected override List<BulletSpawnInfo> BulletPattern
    {
        get
        {
            List<BulletSpawnInfo> bullets = new List<BulletSpawnInfo>();
            // Generate bullets in a circle
            for (int a = 0; a < 360; a += 10)
            {
                bullets.Add(new BulletSpawnInfo()
                {
                    Delay = 0,
                    Direction = new Vector3(Mathf.Cos(Mathf.Deg2Rad * a), 0, Mathf.Sin(Mathf.Deg2Rad * a)),
                    Speed = 12f,
                    PrefabIndex = 0,
                    Lifetime = 0.3f
                });
            }

            return bullets;
        }
    }

    /// <summary>
    /// Create bullets Horizontally and Vertically.
    /// </summary>
    public override void OnExplode()
    {

    }
}
