using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPanel : MonoBehaviour
{
    private Image _towerImage;
    // Start is called before the first frame update
    void Start()
    {
        _towerImage = gameObject.transform.Find("towerImage").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject tower = GameState.instance.towerSelected;
        if(tower != null) {
          _towerImage.sprite = tower.GetComponent<PanelPicture>().sprite;
        }
    }
}
