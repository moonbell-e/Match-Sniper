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
    [SerializeField] private List<Tile> _tiles;
    [SerializeField] private Tile _tilePrefab;

    public Row[] rows;
    public Tile[,] tiles;

    public int Width => tiles.GetLength(0);
    public int Height => tiles.GetLength(1);
    public Vector2Int GridSize => _gridSize;
    public List<Tile> Tiles => _tiles;

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

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.A)) return;

        foreach (var connectedTile in tiles[0, 0].GetConnectedTiles()) connectedTile.gameObject.SetActive(false);
    }
    private void Initialize()
    {
        enabled = true;
    }

    /*public IEnumerator GatherUnits()
    {
        yield return new WaitForSeconds(0.5f);

        foreach (var tile in _tiles)
        {
            if (tile.CurrentUnit != null)
                _units.Add(tile.CurrentUnit);
        }
    }*/
}
