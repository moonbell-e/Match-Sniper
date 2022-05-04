using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool _isOccupied;

    [SerializeField] private Unit _currentUnit;

    public bool IsOccupied => _isOccupied;
    public Unit CurrentUnit => _currentUnit;

    public int x;
    public int y;

    public Tile Left => x > 0 ? GridSystem.Instance.tiles[x - 1, y] : null;
    public Tile Top => y > 0 ? GridSystem.Instance.tiles[x, y - 1] : null;
    public Tile Right => x < GridSystem.Instance.Width - 1 ? GridSystem.Instance.tiles[x + 1, y] : null;
    public Tile Bottom  => y < GridSystem.Instance.Height  - 1 ? GridSystem.Instance.tiles[x, y + 1] : null;

    public Tile[] Neighbours => new[]
    {
        Left,
        Top,
        Right,
        Bottom
    };

    public List<Tile> GetConnectedTiles(List<Tile> exclude = null)
    {
        var result = new List<Tile> { this, };

        if (exclude == null)
        {
            exclude = new List<Tile> { this, };
        }
        else
        {
            exclude.Add(this);
        }

        foreach (var neighbour in Neighbours)
        {
            if (neighbour == null || exclude.Contains(neighbour) || (neighbour.CurrentUnit.Colour != CurrentUnit.Colour && neighbour.CurrentUnit.Type != CurrentUnit.Type)) continue;

            result.AddRange(neighbour.GetConnectedTiles(exclude));
        }

        return result;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            Debug.Log("Occupied!");
            _currentUnit = unit;
            _isOccupied = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            Debug.Log("Not occupied!");
            _currentUnit = null;
            _isOccupied = false;
        }
    }   
}
