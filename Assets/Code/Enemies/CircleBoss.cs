using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBoss : Enemy {

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
            // Generate bullets in a circle
            for (int a = 0; a < 360; a += 15)
            {
                bullets.Add(new BulletSpawnInfo()
                {
                    Delay = 0,
                    Direction = new Vector3(Mathf.Cos(Mathf.Deg2Rad * a), 0, Mathf.Sin(Mathf.Deg2Rad * a)),
                    Speed = 2f,
                    PrefabIndex = 0,
                });
            }

            return bullets;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // Rotate to look at the enemy.
        Vector3 directionToPlayer = (GameObject.FindObjectOfType<PlayerScript>().transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

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

        // Fire streams
        if (m_streamFireTimer < 0)
        {
            m_streamFireTimer = StreamFireRate;
            FireBullet(new BulletSpawnInfo()
            {
                Delay = 0,
                Direction = transform.forward + (transform.right * (Mathf.Sin(Time.realtimeSinceStartup * SineWaveFreq) * SineWaveMag)),
                Speed = 2f,
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
