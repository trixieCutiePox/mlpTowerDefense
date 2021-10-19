using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerSkill: MonoBehaviour
{
    public GameObject projectile;
    public Color projectileColor = Color.white;
    public float cooldown;
    public float range;
    protected Transform temporaryParent;
    private static int nextId = 0;
    private int _id;

    void Awake(){
      temporaryParent = GameObject.Find("Temporary").transform;
      _id = nextId;
      nextId++;
    }

    [HideInInspector]
    public float lastShootTime = 0;

    public abstract void onHit(Collider2D collider, GameObject projectile);

    public abstract void tryShoot(GameObject tower);

    public int GetID(){
      return _id;
    }

    protected GameObject basicShot(GameObject tower, float speed){
      Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), range, 1 << LayerMask.NameToLayer("Enemy"));
      if(colliders.Length > 0 && Time.time > lastShootTime + cooldown) {
        lastShootTime = Time.time;
        Vector3 position = colliders[0].gameObject.transform.position;
        Vector3 direction = (position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        tower.GetComponent<TowerController>().shoot(angle);
        GameObject projectileInstance = Instantiate(projectile, transform.position + direction * 0.3f, Quaternion.AngleAxis(180 - angle, new Vector3(0, 0, 1)), temporaryParent);
        projectileInstance.GetComponent<Rigidbody2D>().velocity = (new Vector2(direction.x, direction.y)) * speed;
        projectileInstance.GetComponent<SpriteRenderer>().color = projectileColor;
        return projectileInstance;
      }
      return null;
    }
}
