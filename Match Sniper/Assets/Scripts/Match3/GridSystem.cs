using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private GridMatch3 _grid;

    [Header("Tiles Settings")]
    [SerializeField] private List<Tile> _tiles;
    [SerializeField] private Tile _tilePrefab;

    public Vector2Int GridSize => _gridSize;
    public GridMatch3 grid => _grid;

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
        _grid.Initialize(_gridSize, _tiles, _tilePrefab);
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
    private void Initialize()
    {
        enabled = true;
    }
}
