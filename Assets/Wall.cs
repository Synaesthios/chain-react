using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
    public void OnTriggerEnter(Collider col)
    {
        if(col.transform.gameObject.tag == "EnemyBullet" || col.transform.gameObject.tag == "PlayerBullet")
        {
            Destroy(col.gameObject);
        }
    }
}
