using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy), typeof(Rigidbody))]
public class WanderMovement : MonoBehaviour {

    public float ChangeDirectionTimerMax = 5f;
    public float ChangeDirectionTimerMin = 2f;

    public float MoveSpeed = 2f;

    private float m_moveTimer;
    private Vector3 m_moveDirection;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (!GetComponent<Enemy>().Alive)
        {
            return;
        }

        if (m_moveTimer <= 0)
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

        GetComponent<Rigidbody>().MovePosition(transform.position + m_moveDirection * MoveSpeed * Time.deltaTime);
    }
}
