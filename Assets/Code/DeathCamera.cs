using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityStandardAssets.Utility.FollowTarget))]
public class DeathCamera : MonoBehaviour
{
	void Start () {
        EventSystem.Subscribe<Events.PlayerDied>(OnPlayerDied);
	}

    private void OnDestroy()
    {
        EventSystem.Unsubscribe<Events.PlayerDied>(OnPlayerDied);
        EventSystem.Unsubscribe<Events.EnemyDied>(OnEnemyDied);
        EventSystem.Unsubscribe<Events.BossDied>(OnBossDied);
    }

    void OnPlayerDied(Events.PlayerDied evt)
    {
        gameObject.SetActive(true);
        GetComponent<UnityStandardAssets.Utility.FollowTarget>().enabled = false;
        EventSystem.Subscribe<Events.EnemyDied>(OnEnemyDied);
        EventSystem.Subscribe<Events.BossDied>(OnBossDied);
    }


    void OnBossDied(Events.BossDied evt)
    {
        UpdateCamera(evt.position);
    }

    void OnEnemyDied(Events.EnemyDied evt)
    {
        UpdateCamera(evt.position);
    }

    void UpdateCamera(Vector3 position)
    {
        position.y = transform.position.y;
        transform.position = position;
    }
}
