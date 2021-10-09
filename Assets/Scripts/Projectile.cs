using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int pierce;
    public int damage;

    void OnTriggerEnter2D(Collider2D collider) {
      collider.gameObject.GetComponent<EnemyController>().hp -= damage;
      pierce--;
      if(pierce == 0) {
        Destroy(gameObject);
      }
    }
}
