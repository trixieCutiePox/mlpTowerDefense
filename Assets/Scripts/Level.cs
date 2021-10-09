using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour
{
    public GameObject monster;
    public Wave wave;
    public GameObject path;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator waveExecutor(int amount, float spacing, float delay){
      yield return new WaitForSeconds(delay);
      for(int i = 0; i < amount; i++){
        GameObject monsterInstance = Instantiate(monster, transform.position, Quaternion.identity);
        monsterInstance.GetComponent<EnemyController>().Initialize(path);
        yield return new WaitForSeconds(spacing);
      }
    }

    public void startLevel(){
      for(int i = 0; i < wave.delays.Length; i++){
        StartCoroutine(waveExecutor(wave.amounts[i], wave.spacing[i], wave.delays[i]));
      }
    }
}
