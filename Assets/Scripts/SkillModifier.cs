using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillModifier : MonoBehaviour
{
    public TowerSkill modifiedSkill;
    public float cooldownMultiplier = 1;

    public void buy(){
      modifiedSkill.cooldownMultiplier *= cooldownMultiplier;
    }
}
