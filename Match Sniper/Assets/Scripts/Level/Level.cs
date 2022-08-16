using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameplayUI _gameplayUI;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Rifle _rifle;
    [SerializeField] private InputSystem _inputSystem;
    [SerializeField] private RifleClip _rifleClip;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private bool _isSpawn;

    private void OnEnable()
    {
        _inputSystem.InputReceived += OnStateReceived;
        if (_isSpawn)
            _unitSpawner.SpawnUnits();
    }

    private void OnDisable()
    {
        _inputSystem.InputReceived -= OnStateReceived;
    }

    public void OnStateReceived(TapState tapState)
    {
        if (tapState == TapState.Pressed)
        {
            _rifle.ShowMatch(_cameraController.ScopeCamera);
        }

        if (tapState == TapState.Released)
        {
            OnShot();
        }
    }

    private void OnShot()
    {
        _rifle.TryShoot(_cameraController.ScopeCamera);
    }
}
