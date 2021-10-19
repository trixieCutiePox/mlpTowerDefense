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
    private int boughtUpgrades;
    private int totalUpgrades;

    int index;
    void Start(){
      index = transform.GetSiblingIndex();
    }

    void traverseUpgradePath(GameObject upgrade, bool previousBought){
      totalUpgrades++;
      GameObject child = null;
      if(upgrade.transform.childCount > 0){
        child = upgrade.transform.GetChild(0).gameObject;
      }
      TowerUpgrade towerUpgrade = upgrade.GetComponent<TowerUpgrade>();
      if(previousBought && !towerUpgrade.bought){
        _upgrade = upgrade;
        if(child != null){
          traverseUpgradePath(child, false);
        }
      }
      if(previousBought && towerUpgrade.bought){
        boughtUpgrades++;
        if(child != null){
          traverseUpgradePath(child, true);
        }
      }
      if(!previousBought){
        if(child != null){
          traverseUpgradePath(child, false);
        }
      }
    }

    void Update() {
      GameObject tower = GameState.instance.towerSelected;
      if(tower != null) {
        if(tower.transform.childCount > index){
            _upgrade = tower.transform.GetChild(index).gameObject;
            boughtUpgrades = 0;
            totalUpgrades = 0;
            traverseUpgradePath(_upgrade, true);
            if(boughtUpgrades < totalUpgrades){
              _price = _upgrade.GetComponent<TowerUpgrade>().price;
              price.text = $"Buy for {_price}";
              upgradeName.text = _upgrade.name;
            } else {
              price.text = "";
              upgradeName.text = "max";
            }
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
