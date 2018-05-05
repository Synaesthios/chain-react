using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    private const float BulletMoveSpeed = 4f;

    private const float ChangeDirectionTimerMax = 5f;
    private const float ChangeDirectionTimerMin = 2f;
    private float m_moveTimer;
    private const float MoveSpeed = 2f;
    private Vector3 m_moveDirection;

    /// <summary>
    /// The directions of all of the fired bullets.
    /// </summary>
    protected override List<BulletSpawnInfo> BulletPattern {
        get {
            return new List<BulletSpawnInfo>()
            {
                new BulletSpawnInfo()
                {
                    Delay = 0,
                    Direction = new Vector3(0, 0, 1),
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
                    Delay = 0,
                    Direction = new Vector3(1, 0, 0),
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
                    Delay = 0.5f,
                    Direction = new Vector3(0, 0, 1),
                    Speed = BulletMoveSpeed,
                    PrefabIndex = 0
                },
                new BulletSpawnInfo()
                {
                    Delay = 0.5f,
                    Direction = new Vector3(0, 0, -1),
                    Speed = BulletMoveSpeed,
                    PrefabIndex = 0
                },
                new BulletSpawnInfo()
                {
                    Delay = 0.5f,
                    Direction = new Vector3(1, 0, 0),
                    Speed = BulletMoveSpeed,
                    PrefabIndex = 0
                },
                new BulletSpawnInfo()
                {
                    Delay = 0.5f,
                    Direction = new Vector3(-1, 0, 0),
                    Speed = BulletMoveSpeed,
                    PrefabIndex = 0
                }
            };
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(!Alive)
        {
            return;
        }
        Move();

        if(Input.GetKeyDown(KeyCode.E))
        {
            FireBulletPattern();
        }
	}

    /// <summary>
    /// Move around
    /// </summary>
    private void Move()
    {
        if(m_moveTimer <= 0)
        {
            m_moveTimer = Random.Range(ChangeDirectionTimerMin, ChangeDirectionTimerMax);
            m_moveDirection = new Vector3(
                Random.Range(-1f, 1f),
                0,
                Random.Range(-1f, 1f));
        }
        else
        {
            m_moveTimer -= Time.deltaTime;
        }

        transform.position += m_moveDirection * MoveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Create bullets Horizontally and Vertically.
    /// </summary>
    public override void OnExplode()
    {
        
    }
}
