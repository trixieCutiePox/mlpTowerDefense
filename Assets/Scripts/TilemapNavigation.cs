using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapNavigation : MonoBehaviour
{
    private Tilemap tilemap;
    private Grid grid;
    private Vector3Int previousPosition;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        grid = gameObject.transform.parent.gameObject.GetComponent<Grid>();
        Debug.Log(tilemap.cellBounds);
        TileBase[] tileArray = tilemap.GetTilesBlock(tilemap.cellBounds);
        for (int index = 0; index < tileArray.Length; index++)
        {
            print(tileArray[index]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButtonDown(0)){
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int posGrid = grid.WorldToCell(pos);
        tilemap.SetTileFlags(posGrid, TileFlags.None);
        if(previousPosition != null){
            tilemap.SetColor(previousPosition, Color.white);
        }
        tilemap.SetColor(posGrid, Color.yellow);
        previousPosition = posGrid;
        //}
    }
}
