using UnityEngine;


public class Level : MonoBehaviour, IInputReceivable
{
    [SerializeField] private GameplayUI _gameplayUI;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Rifle _rifle;
    [SerializeField] private InputSystem _inputSystem;
    [SerializeField] private RifleClip _rifleClip;

    private void OnEnable()
    {
        _inputSystem.InputReceived += OnStateReceived;
    }

    private void OnDisable()
    {
        _inputSystem.InputReceived -= OnStateReceived;
    }

    public void OnStateReceived(TapState tapState)
    {
        if (tapState == TapState.Released)
        {
            OnShot();
        }
    }

    private void OnShot()
    {
        _rifle.ShootUnit(_cameraController.ScopeCamera);
    }
}
