using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPanel : MonoBehaviour
{
    private Image _towerImage;
    private TowerColor _towerImageColor;
    private GameObject _towerPreviousObject;
    private GameObject _panelUpgrades;
    // Start is called before the first frame update
    void Start()
    {
        _towerImage = gameObject.transform.Find("towerImage").GetComponent<Image>();
        _towerImageColor = gameObject.transform.Find("towerImage").GetComponent<TowerColor>();
        _panelUpgrades = gameObject.transform.Find("upgrades").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject tower = GameState.instance.towerSelected;
        if(tower != null) {
          _towerImageColor.sources = tower.GetComponent<TowerColor>().sources;
          _towerImageColor.targets = tower.GetComponent<TowerColor>().targets;
        }
        if(tower != null && _towerPreviousObject != tower) {
          _towerImage.sprite = tower.GetComponent<PanelPicture>().sprite;
          foreach(Transform panelUpgrade in _panelUpgrades.transform){
            panelUpgrade.gameObject.SetActive(true);
          }
        }
        _towerPreviousObject = tower;
    }
}
