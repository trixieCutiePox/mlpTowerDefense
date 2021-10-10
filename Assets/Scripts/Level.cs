using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour
{
    public GameObject monster;
    public Wave wave;
    public GameObject path;
    public Transform temporaryParent;
    private Coroutine[] tasks;

    void Start(){
      temporaryParent = GameObject.Find("Temporary").transform;
    }

    IEnumerator waveExecutor(int amount, float spacing, float delay){
      yield return new WaitForSeconds(delay);
      for(int i = 0; i < amount; i++){
        GameObject monsterInstance = Instantiate(monster, transform.position, Quaternion.identity, temporaryParent);
        monsterInstance.GetComponent<EnemyController>().Initialize(path);
        yield return new WaitForSeconds(spacing);
      }
    }

    public void startLevel(){
      tasks = new Coroutine[wave.delays.Length];
      for(int i = 0; i < wave.delays.Length; i++){
        tasks[i] = StartCoroutine(waveExecutor(wave.amounts[i], wave.spacing[i], wave.delays[i]));
      }
    }

    public void Restart() {
      for(int i = 0; i < tasks.Length; i++) {
        StopCoroutine(tasks[i]);
      }
    }
}
