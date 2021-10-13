using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class TilemapNavigation : MonoBehaviour
{
    private Tilemap tilemap;
    private Grid grid;
    private Vector3Int previousPosition;
    public GameObject tower;
    public Transform temporaryParent;

    static Dictionary<Vector3Int, GameObject> towers = new Dictionary<Vector3Int, GameObject>();

    void Start()
    {
        temporaryParent = GameObject.Find("Temporary").transform;
        tilemap = GetComponent<Tilemap>();
        grid = gameObject.transform.parent.gameObject.GetComponent<Grid>();
    }

    void placeTower(Vector3 position, Vector3Int posGrid){
      GameObject towerInstance = Instantiate(tower, position, Quaternion.identity, temporaryParent);
      towers.Add(posGrid, towerInstance);
    }

    public static void sellTower(GameObject towerInstance){
      //TODO vector is not nullable...(better solution?)
      Vector3Int key = new Vector3Int(0, 0, -100);
      foreach(KeyValuePair<Vector3Int, GameObject> entry in towers){
        if(entry.Value == towerInstance){
          key = entry.Key;
        }
      }

      if(key.z != -100) {
        GameState.instance.cash += towerInstance.GetComponent<TowerUpgrades>().sellValue;

        Destroy(towerInstance);
        towers.Remove(key);
      }
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseControl.gameIsPaused) return;
        if (EventSystem.current.IsPointerOverGameObject())
        {
          return;
        }
        int cost = 150;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int posGrid = grid.WorldToCell(pos);
        Vector3Int posGrid2 = posGrid;
        posGrid2.x++;
        posGrid2.y++;
        Vector3 positionSnapped = grid.CellToWorld(posGrid);
        Vector3 positionSnapped2 = grid.CellToWorld(posGrid2);
        Vector3 positionSnappedMiddle = (positionSnapped + positionSnapped2) / 2;
        tilemap.SetTileFlags(posGrid, TileFlags.None);
        if(previousPosition != null){
            tilemap.SetColor(previousPosition, Color.white);
        }
        tilemap.SetColor(posGrid, Color.yellow);
        previousPosition = posGrid;
        TileBase tile = tilemap.GetTile(posGrid);
        if(tile == null){
          if(Input.GetMouseButtonDown(0)){
            GameState.instance.towerSelected = null;
          }
          return;
        }
        if(tile is RoadTile){
          tilemap.SetColor(posGrid, Color.red);
          return;
        }

        if(Input.GetMouseButtonDown(0)){
          if(towers.ContainsKey(posGrid)){
            GameState.instance.towerSelected = towers[posGrid];
            return;
          }
          GameState.instance.towerSelected = null;
          if(GameState.instance.cash >= cost){
            GameState.instance.cash -= cost;
            placeTower(positionSnappedMiddle, posGrid);
          }
        }
    }
}
