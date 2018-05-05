using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector3 m_movementVector;
    
    /// <summary>
    /// When the  bullet is created by an enemy
    /// </summary>
    /// <param name="movementVector"></param>
	public void Initialize(Vector3 movementVector)
    {
        m_movementVector = movementVector;
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
        transform.position += m_movementVector * Time.deltaTime;
    }
}
