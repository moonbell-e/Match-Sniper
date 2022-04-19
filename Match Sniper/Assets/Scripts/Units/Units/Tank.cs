using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Unit
{
    [Header("Colour Settings")]
    [SerializeField] private MeshRenderer _body;

    public MeshRenderer Body => _body;
}
