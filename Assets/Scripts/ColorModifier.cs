using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorModifier : MonoBehaviour
{
    public Color[] targets;
    public TowerColor towerColor;

    public void buy(){
      towerColor.targets = targets;
    }
}
