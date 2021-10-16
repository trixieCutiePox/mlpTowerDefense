using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerSkill: MonoBehaviour
{
    public GameObject projectile;
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
}
