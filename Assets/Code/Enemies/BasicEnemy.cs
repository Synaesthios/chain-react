using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{

    /// <summary>
    /// The type of bullet that I spawn.
    /// </summary>
    public GameObject BulletPrefab;

    private const float BulletMoveSpeed = 4f;

    private const float ChangeDirectionTimerMax = 5f;
    private const float ChangeDirectionTimerMin = 2f;
    private float m_moveTimer;
    private const float MoveSpeed = 2f;
    private Vector3 m_moveDirection;

    /// <summary>
    /// The directions of all of the fired bullets.
    /// </summary>
    List<Vector3> FireVectors = new List<Vector3>()
    {
        new Vector3(0, 0, 1) * BulletMoveSpeed,
        new Vector3(0, 0, -1) * BulletMoveSpeed,
        new Vector3(1, 0, 0) * BulletMoveSpeed,
        new Vector3(-1, 0, 0) * BulletMoveSpeed,
    };

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Move();
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
        foreach(var vector in FireVectors)
        {
            var bulletObject = GameObject.Instantiate(BulletPrefab) as GameObject;
            EnemyBullet bullet = bulletObject.GetComponent<EnemyBullet>();
            bullet.Initialize(vector);
        }
    }
}
