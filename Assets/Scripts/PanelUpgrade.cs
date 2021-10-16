using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUpgrade : MonoBehaviour
{
    public Text upgradeName;
    public Text price;
    private int _price;
    private GameObject _upgrade;

    int index;
    void Start(){
      index = transform.GetSiblingIndex();
    }

    void Update() {
      GameObject tower = GameState.instance.towerSelected;
      if(tower != null) {
        if(tower.transform.childCount > index){
            _upgrade = tower.transform.GetChild(index).gameObject;
            _price = _upgrade.GetComponent<TowerUpgrade>().price;
            price.text = $"Buy for {_price}";
            upgradeName.text = _upgrade.name;
        } else {
          gameObject.SetActive(false);
        }
      }
    }

    public void buy(){
      GameObject tower = GameState.instance.towerSelected;
      tower.GetComponent<TowerController>().upgrade(_upgrade);
    }
}
