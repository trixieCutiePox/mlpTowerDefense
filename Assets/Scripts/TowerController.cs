using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TowerController : MonoBehaviour
{
    public float range;
    private AngleBasedRenderer angleBasedRenderer;

    void Start()
    {
        angleBasedRenderer = GetComponent<AngleBasedRenderer>();
        foreach(Transform upgrade in transform){
          TowerSkill[] towerSkills = upgrade.gameObject.GetComponents<TowerSkill>();
          foreach(TowerSkill skill in towerSkills){
            Debug.Log(skill.GetID());
          }
        }
    }

    public void shoot(float angle){
      angleBasedRenderer.SetAngle(180 + angle);
    }

    List<TowerSkill> getActiveSkills(){
      List<TowerSkill> skills = new List<TowerSkill>();
      TowerSkill skillBase = GetComponent<TowerSkill>();
      skills.Add(skillBase);
      foreach(Transform upgrade in transform){
        if(upgrade.gameObject.GetComponent<TowerUpgrade>().bought){
          if(upgrade.gameObject.TryGetComponent(out TowerSkill newSkill)){
            skills.Add(newSkill);
          }
        }
      }
      return skills;
    }

    void Update()
    {
      List<TowerSkill> skills = getActiveSkills();
      foreach(TowerSkill skill in skills){
        skill.tryShoot(gameObject);
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
