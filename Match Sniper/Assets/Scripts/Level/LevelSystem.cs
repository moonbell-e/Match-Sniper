using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelSystem : MonoBehaviour, IInputReceivable
{
    [SerializeField] private GameplayUI _gameplayUI;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Rifle _rifle;
    [SerializeField] private InputSystem _inputSystem;
    [SerializeField] private BulletSystem _bulletSystem;

    private void Awake()
    {
        _inputSystem.InputReceived += OnStateReceived;
    }

    private void OnDisable()
    {
        _inputSystem.InputReceived -= OnStateReceived;
    }
    public void OnStateReceived(GunState gunState)
    {
        if (gunState == GunState.Shoot)
        {
            OnShot();
        }
    }
    private void OnShot()
    {
        if (_bulletSystem.Bullets.Count > 0)
        {
            _rifle.CalculateBulletTarget(_cameraController.ScopeCamera);
            _bulletSystem.Bullets.Remove(_bulletSystem.GetLastBullet()); 
        }
        else
            Debug.Log("No bullets!");
    }
}
