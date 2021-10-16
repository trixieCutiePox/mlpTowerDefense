using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillModifier : MonoBehaviour
{
    public TowerSkill modifiedSkill;

    void Start(){
      Debug.Log(modifiedSkill.GetID());
    }
}
