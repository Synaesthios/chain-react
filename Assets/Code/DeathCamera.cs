using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityStandardAssets.Utility.FollowTarget))]
public class DeathCamera : MonoBehaviour
{
	void Start () {
        EventSystem.Subscribe<Events.PlayerDied>(OnPlayerDied);
	}
	
    void OnPlayerDied(Events.PlayerDied evt)
    {
        gameObject.SetActive(true);
        GetComponent<UnityStandardAssets.Utility.FollowTarget>().enabled = false;
        EventSystem.Subscribe<Events.EnemyDied>(OnEnemyDied);
    }

    void OnEnemyDied(Events.EnemyDied evt)
    {
        var position = transform.position;
        position.x = evt.position.x;
        position.z = evt.position.z;
        transform.position = position;
    }
}
