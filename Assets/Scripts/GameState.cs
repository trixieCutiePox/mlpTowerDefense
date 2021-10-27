using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState: MonoBehaviour
{
  public static GameState instance = null;

  public int startingCash;
  public int startingHp;

  public GameObject hpTextObject;
  public GameObject cashTextObject;
  public GameObject restartScreen;
  public GameObject towerPanel;
  public GameObject startButton;

  public GameObject levelObject;

  private Text hpText;
  private Text cashText;
  private Level level;

  public int currentLevel {get; set;}

  private bool _win;
  public bool win {
    get {return _win;}
    set {
      _win = value;
      if(value){
        restartScreen.transform.Find("RestartText").GetComponent<Text>().text = "You won!";
        restartScreen.SetActive(true);
        PauseControl.PauseGame(true);
      }
    }
  }

  private bool _levelInProgress;
  public bool levelInProgress {
    get { return _levelInProgress; }
    set {
      _levelInProgress = value;
      if(value) {
        startButton.SetActive(false);
      } else {
        startButton.SetActive(true);
      }
    }
  }

  private int _hp;
  public int hp {
    get { return _hp; }
    set {
      _hp = value;
       hpText.text = value.ToString();
       if(value <= 0) {
         restartScreen.transform.Find("RestartText").GetComponent<Text>().text = "You lost";
         restartScreen.SetActive(true);
         PauseControl.PauseGame(true);
       }
     }
  }

  private int _cash;
  public int cash {
    get { return _cash; }
    set {
      _cash = value;
       cashText.text = value.ToString();
     }
  }

  private GameObject _towerSelected;
  public GameObject towerSelected {
    get { return _towerSelected; }
    set {
      _towerSelected = value;
      if(value != null){
        towerPanel.SetActive(true);
      } else {
        towerPanel.SetActive(false);
      }
    }
  }
  // Start is called before the first frame update
  void Start()
  {
    hpText = hpTextObject.GetComponent<Text>();
    cashText = cashTextObject.GetComponent<Text>();
    level = levelObject.GetComponent<Level>();
    cash = startingCash;
    hp = startingHp;
  }

  private void Awake()
  {
      if (instance == null)
      {
          instance = this;
      }
      else if (instance != this)
      {
          Debug.Log("Instance already exists, destroying object!");
          Destroy(this);
      }
  }

  public void Restart() {
    restartScreen.SetActive(false);
    hp = startingHp;
    cash = startingCash;
    win = false;
    currentLevel = 0;
    towerSelected = null;
    foreach (Transform child in GameObject.Find("Temporary").transform) {
      Destroy(child.gameObject);
    }
    level.Restart();
    PauseControl.PauseGame(false);
    TilemapNavigation.towers.Clear();
  }


}
