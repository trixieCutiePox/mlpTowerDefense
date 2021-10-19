using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnergyShot : TowerSkill
{
  public int pierce;
  public int damage;
  public float speed;
  public Dictionary<GameObject, int> hitCounter = new Dictionary<GameObject, int>();

  public override void onHit(Collider2D collider, GameObject projectile){
    if(hitCounter.ContainsKey(collider.gameObject)){
      return;
    }
    hitCounter[collider.gameObject] = 1;
    collider.gameObject.GetComponent<EnemyController>().hp -= damage;
    pierce--;
    if(pierce == 0) {
      GameObject.Destroy(projectile);
    }
  }

  public override void tryShoot(GameObject tower){
    GameObject projectileInstance = basicShot(tower, speed);
    if(projectileInstance != null){
      EnergyShot shot = projectileInstance.AddComponent<EnergyShot>();
      shot.pierce = pierce;
      shot.damage = damage;
    }
  }
}
