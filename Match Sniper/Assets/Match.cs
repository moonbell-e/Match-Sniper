using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private List<Unit> _matchs;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
    }

    public void LightSubstracts()
    {
        _unit.OutlineSubstrate();
        foreach (var unit in _matchs)
        {
            unit.OutlineSubstrate();
        }
    }
}
