using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _requiredUnits;
    [SerializeField] private Transform _spawnPoint;

    public void SpawnUnits()
    {
        int i = 0;
        for (int x = 0; x < GridSystem.Instance.GridSize.x; x++)
        {
            for (int y = 0; y < GridSystem.Instance.GridSize.y; y++)
            {
                if (_requiredUnits.Count != i)
                {
                    Instantiate(_requiredUnits[i], new Vector3Int(x, 0, y), _requiredUnits[i].transform.rotation, _spawnPoint);
                    i++;
                }
            }
        }
    }
}
