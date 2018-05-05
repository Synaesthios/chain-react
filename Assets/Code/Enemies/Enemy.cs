using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    
    public int Health;

    /// <summary>
    /// Called on each enemy type when they run out of health and need to explode.
    /// </summary>
    public abstract void OnExplode();

    /// <summary>
    /// When colliding with something, maybe explode.
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision)
    {
        
    }

    private void Explode()
    {
        // Call the child override function.
        OnExplode();

        // Destroy myself.
        Destroy(gameObject);
    }
}
