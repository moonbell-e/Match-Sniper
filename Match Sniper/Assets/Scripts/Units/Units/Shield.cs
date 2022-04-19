using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Unit
{
    [Header("Colour Settings")]
    [SerializeField] private SkinnedMeshRenderer _body;
    [SerializeField] private SkinnedMeshRenderer _helmet;

    public SkinnedMeshRenderer Body => _body;
    public SkinnedMeshRenderer Helmet => _helmet;
}
