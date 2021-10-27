using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour
{
    public GameObject monster;
    public Wave[] waves;
    public GameObject path;
    public Transform temporaryParent;
    private Coroutine[] tasks;
    private bool[] finished;
    private List<GameObject> monsters;

    void Start(){
      temporaryParent = GameObject.Find("Temporary").transform;
      monsters = new List<GameObject>();
    }

    IEnumerator waveExecutor(int index, int amount, float spacing, float delay){
      yield return new WaitForSeconds(delay);
      for(int i = 0; i < amount; i++){
        GameObject monsterInstance = Instantiate(monster, transform.position, Quaternion.identity, temporaryParent);
        monsterInstance.GetComponent<EnemyController>().Initialize(path);
        yield return new WaitForSeconds(spacing);
      }
      finished[index] = true;
    }

    public void startLevel(){
      Wave wave = waves[GameState.instance.currentLevel];
      tasks = new Coroutine[wave.delays.Length];
      finished = new bool[wave.delays.Length];
      GameState.instance.levelInProgress = true;
      for(int i = 0; i < wave.delays.Length; i++){
        finished[i] = false;
        tasks[i] = StartCoroutine(waveExecutor(i, wave.amounts[i], wave.spacing[i], wave.delays[i]));
      }
    }

    public void Restart() {
      for(int i = 0; i < tasks.Length; i++) {
        StopCoroutine(tasks[i]);
      }
      GameState.instance.levelInProgress = false;
      monsters.Clear();
    }

    void Update() {
      if(GameState.instance.levelInProgress) {
        bool allDestroyed = true;
        foreach(GameObject monsterInstance in monsters) {
          if(monsterInstance != null) {
            allDestroyed = false;
          }
        }
        bool sendingFinished = true;
        foreach(bool finishedCoroutine in finished){
          sendingFinished = sendingFinished && finishedCoroutine;
        }

        if(allDestroyed && sendingFinished){
          GameState.instance.levelInProgress = false;
          GameState.instance.cash += 100;
          GameState.instance.currentLevel++;
          if(GameState.instance.currentLevel == waves.Length){
            GameState.instance.win = true;
          }
        }
      }
    }
}
