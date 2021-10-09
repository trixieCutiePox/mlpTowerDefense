using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public float range;
    private AngleBasedRenderer angleBasedRenderer;
    public GameObject projectile;
    public float lastShootTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        angleBasedRenderer = GetComponent<AngleBasedRenderer>();
    }

    void shoot(Vector3 position){
      Vector3 direction = (position - transform.position).normalized;
      Debug.Log(Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
      float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
      angleBasedRenderer.SetAngle(180 + angle);
      GameObject projectileInstance = Instantiate(projectile, transform.position + direction * 0.3f, Quaternion.AngleAxis(180 - angle, new Vector3(0, 0, 1)));
      projectileInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y);
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), range, 1 << LayerMask.NameToLayer("Enemy"));
        if(colliders.Length > 0 && Time.time > lastShootTime + 1) {
          lastShootTime = Time.time;
          shoot(colliders[0].gameObject.transform.position);
        }
    }
}
