using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossBullet : EnemyBullet
{
    private void OnValidate()
    {
        gameObject.tag = "BossBullet";
        gameObject.layer = LayerMask.NameToLayer("BossBullets");
    }
}
