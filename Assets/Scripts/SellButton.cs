using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellButton : MonoBehaviour
{
    private Text text;
    void Start(){
      text = gameObject.transform.Find("Text").GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        GameObject tower = GameState.instance.towerSelected;
        if(tower != null) {
          TowerUpgrades towerUpgrades = tower.GetComponent<TowerUpgrades>();
          text.text = $"Sell for {towerUpgrades.sellValue}";
        }
    }

    public void Sell(){
      TilemapNavigation.sellTower(GameState.instance.towerSelected);
      GameState.instance.towerSelected = null;
    }
}
