using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
      StartCoroutine(destroyAfterTime());
    }

    IEnumerator destroyAfterTime(){
      yield return new WaitForSeconds(lifetime);
      Destroy(gameObject);
    }
}
