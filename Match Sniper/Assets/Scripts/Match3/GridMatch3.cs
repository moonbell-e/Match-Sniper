using System.Collections.Generic;
using UnityEngine;

public class GridMatch3 : MonoBehaviour
{
    private Vector2Int _gridSize;

    public void Initialize(Vector2Int size, List<Tile> tiles, Tile tilePrefab)
    {
        _gridSize = size;
        InitTiles(tiles, tilePrefab);
    }

    private void OnDrawGizmosSelected()
    {
        for(int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }
   
    public void InitTiles(List<Tile> tiles, Tile tilePrefab)
    {
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                Tile currentTile = Instantiate(tilePrefab, new Vector3Int(x, 0, y), Quaternion.identity, gameObject.transform);
                tiles.Add(currentTile);
            }
        }
    }
}
