using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TowerController : MonoBehaviour
{
    public float range;
    private AngleBasedRenderer angleBasedRenderer;
    private TowerUpgrade[] upgrades;
    private TowerSkill[] towerSkills;
    private TowerSkill mainSkill;

    void Start()
    {
        angleBasedRenderer = GetComponent<AngleBasedRenderer>();
        upgrades = GetComponentsInChildren<TowerUpgrade>();
        towerSkills = new TowerSkill[upgrades.Length];
        mainSkill = GetComponent<TowerSkill>();
        for(int i = 0; i < upgrades.Length; i++){
          TowerUpgrade upgrade = upgrades[i];
          upgrade.gameObject.TryGetComponent(out TowerSkill towerSkill);
          towerSkills[i] = towerSkill;
        }
    }

    public void shoot(float angle){
      angleBasedRenderer.SetAngle(180 + angle);
    }

    void Update()
    {
      mainSkill.tryShoot(gameObject);
      for(int i = 0; i < upgrades.Length; i++){
        if(upgrades[i].bought){
          if(towerSkills[i] != null){
            towerSkills[i].tryShoot(gameObject);
          }
        }
      }
    }

    public void upgrade(GameObject _upgrade){
      TowerUpgrade towerUpgrade = _upgrade.GetComponent<TowerUpgrade>();
      if(towerUpgrade.price > GameState.instance.cash || towerUpgrade.bought){
        return;
      }
      GameState.instance.cash -= towerUpgrade.price;
      towerUpgrade.bought = true;
    }
}
