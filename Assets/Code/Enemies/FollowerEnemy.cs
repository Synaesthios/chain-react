using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemy : Enemy
{
    private const float BulletMoveSpeed = 4f;

    private const float MoveSpeed = 1.5f;

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
                    Speed = BulletMoveSpeed,
                    PrefabIndex = 0,
                    Lifetime = 1f
                });
            }

            return bullets;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.F))
        {
            FireBulletPattern();
        }
    }

    /// <summary>
    /// Move around
    /// </summary>
    private void Move()
    {
        // Get vector to player.
        PlayerScript player = GameObject.FindObjectOfType<PlayerScript>();
        if(player == null) { return; }
        Vector3 vectorToPlayer = (player.transform.position - transform.position).normalized;

        // Move along the path to the player.
        transform.position += vectorToPlayer * MoveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Create bullets Horizontally and Vertically.
    /// </summary>
    public override void OnExplode()
    {

    }
}
