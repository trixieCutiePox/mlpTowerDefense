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
  public Color projectileColor = Color.white;

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
    Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), range, 1 << LayerMask.NameToLayer("Enemy"));
    if(colliders.Length > 0 && Time.time > lastShootTime + cooldown) {
      lastShootTime = Time.time;
      Vector3 position = colliders[0].gameObject.transform.position;
      Vector3 direction = (position - transform.position).normalized;
      float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
      tower.GetComponent<TowerController>().shoot(angle);
      GameObject projectileInstance = Instantiate(projectile, transform.position + direction * 0.3f, Quaternion.AngleAxis(180 - angle, new Vector3(0, 0, 1)), temporaryParent);
      EnergyShot shot = projectileInstance.AddComponent<EnergyShot>();
      shot.pierce = pierce;
      shot.damage = damage;
      projectileInstance.GetComponent<Rigidbody2D>().velocity = (new Vector2(direction.x, direction.y)) * speed;
      projectileInstance.GetComponent<SpriteRenderer>().color = projectileColor;
    }
  }
}
