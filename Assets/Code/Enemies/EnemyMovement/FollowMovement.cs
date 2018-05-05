using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class FollowMovement : MonoBehaviour {

    public float MoveSpeed = 1.5f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!GetComponent<Enemy>().Alive)
        {
            return;
        }

        // Get vector to player.
        PlayerScript player = GameObject.FindObjectOfType<PlayerScript>();
        if (player == null) { return; }
        Vector3 vectorToPlayer = (player.transform.position - transform.position).normalized;

        // Move along the path to the player.
        transform.position += vectorToPlayer * MoveSpeed * Time.deltaTime;
    }
}
