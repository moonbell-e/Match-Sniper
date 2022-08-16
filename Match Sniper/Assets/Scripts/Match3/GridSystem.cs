using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private GridMatch3 _grid;

    [Header("Tiles Settings")]
    [SerializeField] private List<Unit> _units;
    [SerializeField] private List<Tile> _connectedTiles;
    [SerializeField] private Tile _tilePrefab;

    private readonly List<Tile> _selection = new List<Tile>();

    public Row[] rows;
    public Tile[,] tiles;

    public int Width => tiles.GetLength(0);
    public int Height => tiles.GetLength(1);
    public Vector2Int GridSize => _gridSize;

    #region Singleton Init
    private static GridSystem _instance;
    private void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
        {
            Debug.Log($"Destroying {gameObject.name}, caused by one singleton instance");
            Destroy(gameObject);
        }
        //_grid.Initialize(_gridSize, _tiles, _tilePrefab);
    }

    public static GridSystem Instance // Init not in order
    {
        get
        {
            if (_instance == null)
                Init();
            return _instance;
        }
        private set { _instance = value; }
    }
    static void Init() // Init script
    {
        _instance = FindObjectOfType<GridSystem>();
        if (_instance != null)
            _instance.Initialize();
    }
    #endregion

    private void Start()
    {
        tiles = new Tile[rows.Max(row => row.tiles.Length), rows.Length];

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var tile = rows[y].tiles[x];

                tiles[x, y] = tile;

                tile.x = x;

                tile.y = y;
            }
        }
    }

    private void Initialize()
    {
        enabled = true;
    }

    public void Select(Tile tile)
    {
        if (!_selection.Contains(tile)) _selection.Add(tile);

        Debug.Log($"Selected tile at  {_selection[0].x}, {_selection[0].y}");

        if(TryMatch())
        {
            tile.CurrentUnit.OutlineSubstrate();
        }
        else
        {
            Debug.Log("Can't");
        }

        _selection.Clear();
    }

    private bool TryMatch()
    {
        for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
                if (tiles[x, y].GetConnectedTiles().Skip(1).Count() >= 2)
                    return true;

        return false;
    }

    public void Match(Tile tile)
    {
        _connectedTiles = tile.GetConnectedTiles();

        if (_connectedTiles.Skip(1).Count() < 2) return;

        foreach (var connectedTile in _connectedTiles)
        {
            connectedTile.CurrentUnit.TakeDamage(1);
        }
    }

    //public IEnumerator GatherUnits()
    //{
    //    yield return new WaitForSeconds(0.5f);

    //    foreach (var tile in _tiles)
    //    {
    //        if (tile.CurrentUnit != null)
    //            _units.Add(tile.CurrentUnit);
    //    }
    //}
}
