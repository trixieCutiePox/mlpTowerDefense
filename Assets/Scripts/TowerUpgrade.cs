using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    public int price;
    [HideInInspector]
    public bool bought = false;

    //called by sendMessage
    public void buy(){
      bought = true;
    }
}
