using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int hp;
    public float speed;

    private int currentPathSegmentIndex = 0;
    private Vector2[] pathSegments;
    private Rigidbody2D rigidbody2D;
    private AngleBasedRenderer angleBasedRenderer;
    // Start is called before the first frame update
    public void Initialize(GameObject path)
    {
      pathSegments = new Vector2[path.transform.childCount];
      rigidbody2D = GetComponent<Rigidbody2D>();
      angleBasedRenderer = GetComponent<AngleBasedRenderer>();
      for(int i = 0; i < path.transform.childCount; i++){
        pathSegments[i] = path.transform.Find(i.ToString()).position;
      }
    }

    void FixedUpdate(){
      float distance = speed * Time.fixedDeltaTime;
      Vector2 diff = pathSegments[currentPathSegmentIndex] - rigidbody2D.position;
      Vector2 direction = diff.normalized;
      Vector2 target = rigidbody2D.position + distance * direction;

      if(diff.magnitude < distance){
        if(currentPathSegmentIndex < pathSegments.Length - 1){
          float additionalDistance = distance - diff.magnitude;
          target = (pathSegments[currentPathSegmentIndex + 1] - pathSegments[currentPathSegmentIndex]).normalized * additionalDistance + pathSegments[currentPathSegmentIndex];
          currentPathSegmentIndex++;
        } else {
          Destroy(gameObject);
        }
      }

      Vector2 finalDirection = target - rigidbody2D.position;

      angleBasedRenderer.SetAngle(180 + Mathf.Atan2(finalDirection.x, finalDirection.y) * Mathf.Rad2Deg);
      rigidbody2D.MovePosition(target);
    }

    // Update is called once per frame
    void Update()
    {
      if(hp <= 0) {
        Destroy(gameObject);
      }
    }
}
