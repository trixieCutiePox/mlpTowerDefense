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
  public GameObject loseScreen;
  public GameObject towerPanel;

  public GameObject levelObject;

  private Text hpText;
  private Text cashText;
  private Level level;

  private int _hp;
  public int hp {
    get { return _hp; }
    set {
      _hp = value;
       hpText.text = value.ToString();
       if(value <= 0) {
         loseScreen.SetActive(true);
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
    loseScreen.SetActive(false);
    hp = startingHp;
    cash = startingCash;
    foreach (Transform child in GameObject.Find("Temporary").transform) {
      Destroy(child.gameObject);
    }
    level.Restart();
    PauseControl.PauseGame(false);
  }


}
