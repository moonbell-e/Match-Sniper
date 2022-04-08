using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Unit", menuName = "Scriptable Objects/New Unit")]
public class UnitDataSO : ScriptableObject
{
    [Header("Unit View")]
    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private UnitType _type;
    [SerializeField] private UnitColour _colour;

    [Header("Unit Attributes")]
    [SerializeField] private int _health;
    [SerializeField] private int _gridSize;

    public UnitType Type => _type;
    public UnitColour Colour => _colour;
    public int Health => _health;
}
