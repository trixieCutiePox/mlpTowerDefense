using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : TowerSkill
{
    public int damage;
    public float speed;

    public float blastRadius;
    public GameObject explosion;

    public override void onHit(Collider2D collider, GameObject projectile){
      Collider2D[] colliders = Physics2D.OverlapCircleAll(
        new Vector2(projectile.transform.position.x, projectile.transform.position.y),
        blastRadius,
        1 << LayerMask.NameToLayer("Enemy")
      );
      foreach(Collider2D enemy in colliders){
        enemy.gameObject.GetComponent<EnemyController>().hp -= damage;
      }
      Instantiate(explosion, projectile.transform.position, Quaternion.identity);
      Destroy(projectile);
    }

    public override void tryShoot(GameObject tower){
      GameObject projectileInstance = basicShot(tower, speed);
      if(projectileInstance != null){
        Fireball shot = projectileInstance.AddComponent<Fireball>();
        shot.damage = damage;
        shot.blastRadius = blastRadius;
        shot.explosion = explosion;
      }
    }
}
