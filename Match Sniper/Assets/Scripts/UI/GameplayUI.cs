using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private GameObject _scopeOverlay;

    public void ShowScopeOverlay()
    {
        _scopeOverlay.SetActive(true);
    }
    
    public void HideScopeOverlay()
    {
        _scopeOverlay.SetActive(false);
    }
}
