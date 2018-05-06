using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
    public void OnTriggerEnter(Collider col)
    {
        if (col.transform.gameObject.tag == "EnemyBullet")
        {
            col.GetComponent<EnemyBullet>().Die();
        }
        else if (col.transform.gameObject.tag == "PlayerBullet")
        {
            Destroy(col.gameObject);
        }
        else if (col.transform.gameObject.tag == "BossBullet")
        {
            Destroy(col.gameObject);
        }
    }
}
