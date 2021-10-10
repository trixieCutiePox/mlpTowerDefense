using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapNavigation : MonoBehaviour
{
    private Tilemap tilemap;
    private Grid grid;
    private Vector3Int previousPosition;
    public GameObject tower;
    public Transform temporaryParent;
    
    void Start()
    {
        temporaryParent = GameObject.Find("Temporary").transform;
        tilemap = GetComponent<Tilemap>();
        grid = gameObject.transform.parent.gameObject.GetComponent<Grid>();
    }

    void placeTower(Vector3 position){
      Instantiate(tower, position, Quaternion.identity, temporaryParent);
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseControl.gameIsPaused) return;
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
          return;
        }
        if(tile is RoadTile){
          return;
        }

        if(Input.GetMouseButtonDown(0) && GameState.instance.cash >= cost){
          GameState.instance.cash -= cost;
          placeTower(positionSnappedMiddle);
        }
    }
}
