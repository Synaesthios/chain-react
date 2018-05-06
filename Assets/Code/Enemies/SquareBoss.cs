using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBoss : Enemy
{

    private const float PulseFireRate = 2f;
    private float m_pulseFireTimer = 0f;

    private const float StreamFireRate = 0.2f;
    private float m_streamFireTimer = 0f;
    private const float SineWaveFreq = 1f;
    private const float SineWaveMag = 0.25f;

    /// <summary>
    /// The AOE Pulse bullets
    /// </summary>
    protected override List<BulletSpawnInfo> BulletPattern
    {
        get
        {
            List<BulletSpawnInfo> bullets = new List<BulletSpawnInfo>();
            // Generate bullets in a square
            for (int i = -4; i < 4; i++)
            {
                Vector3 offset = new Vector3(i, 0, 4);
                bullets.Add(new BulletSpawnInfo()
                {
                    StartOffset = offset,
                    Direction = offset,
                    Speed = 1f
                });
            }
            for (int i = -4; i < 4; i++)
            {
                Vector3 offset = new Vector3(i, 0, -4);
                bullets.Add(new BulletSpawnInfo()
                {
                    StartOffset = offset,
                    Direction = offset,
                    Speed = 1f
                });
            }

            for (int i = -4; i < 4; i++)
            {
                Vector3 offset = new Vector3(4, 0, i);
                bullets.Add(new BulletSpawnInfo()
                {
                    StartOffset = offset,
                    Direction = offset,
                    Speed = 1f
                });
            }
            for (int i = -4; i < 4; i++)
            {
                Vector3 offset = new Vector3(-4, 0, i);
                bullets.Add(new BulletSpawnInfo()
                {
                    StartOffset = offset,
                    Direction = offset,
                    Speed = 1f
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
        // Pulse AOE
        if (m_pulseFireTimer < 0)
        {
            m_pulseFireTimer = PulseFireRate;
            FireBulletPattern();
        }
        else
        {
            m_pulseFireTimer -= Time.deltaTime;
        }

        // Rotate to look at the enemy.
        if (GameObject.FindObjectOfType<PlayerScript>() == null)
        {
            return;
        }
        Vector3 directionToPlayer = (GameObject.FindObjectOfType<PlayerScript>().transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

        // Fire streams
        if (m_streamFireTimer < 0)
        {
            m_streamFireTimer = StreamFireRate;
            FireBullet(new BulletSpawnInfo()
            {
                Delay = 0,
                Direction = transform.forward + (transform.right * (Mathf.Sin(Time.realtimeSinceStartup * SineWaveFreq) * SineWaveMag)),
                Speed = 3f,
                PrefabIndex = 1,
                StartOffset = transform.forward * 4
            });
        }
        else
        {
            m_streamFireTimer -= Time.deltaTime;
        }
    }



    public override void OnExplode()
    {

    }
}
