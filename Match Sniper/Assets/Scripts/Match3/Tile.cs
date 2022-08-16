using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Unit _currentUnit;

    [SerializeField] private List<Tile> result;

    public Unit CurrentUnit => _currentUnit;

    public int x;
    public int y;

    public Tile Left;
    public Tile Top; 
    public Tile Right; 
    public Tile Bottom; 

    public List<Tile> Neighbours;

    private void Start()
    {
        _currentUnit = GetComponent<Unit>();
        StartCoroutine(CollectNeighbours());
    }
    public List<Tile> GetConnectedTiles(List<Tile> exclude = null)
    {
        result = new List<Tile> { this, };

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
            if (neighbour == null || exclude.Contains(neighbour) || neighbour.CurrentUnit.Type != _currentUnit.Type) continue;

            result.AddRange(neighbour.GetConnectedTiles(exclude));
        }

        return result;
    }

    private IEnumerator CollectNeighbours()
    {
        yield return new WaitForSeconds(0.1f);

        Left = x > 0 ? GridSystem.Instance.tiles[x - 1, y] : null;
        Top = y > 0 ? GridSystem.Instance.tiles[x, y - 1] : null;
        Right = x < GridSystem.Instance.Width - 1 ? GridSystem.Instance.tiles[x + 1, y] : null;
        Bottom = y < GridSystem.Instance.Height - 1 ? GridSystem.Instance.tiles[x, y + 1] : null;

        Neighbours.Add(Left);
        Neighbours.Add(Top);
        Neighbours.Add(Right);
        Neighbours.Add(Bottom);
    }
}
