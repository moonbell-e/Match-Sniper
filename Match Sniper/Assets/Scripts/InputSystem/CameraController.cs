using UnityEngine;

public class CameraController : MonoBehaviour, IInputReceivable
{
    [SerializeField] private InputSystem _inputSystem;
    [SerializeField] private GameplayUI _gameplayUI;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Camera _scopeCamera;

    [SerializeField] private bool _limitAngles;
    [SerializeField] private float _xMinAngle;
    [SerializeField] private float _xMaxAngle;
    [SerializeField] private float _yMinAngle;
    [SerializeField] private float _yMaxAngle;

    public Camera MainCamera => _mainCamera;
    public Camera ScopeCamera => _scopeCamera;

    private void Awake()
    {
        _mainCamera.enabled = true;
        _scopeCamera.enabled = false;
        _inputSystem.InputReceived += OnStateReceived;
        _inputSystem.InputDeltaReceived += OnInputReceived;
    }

    private void OnDestroy()
    {
        _inputSystem.InputReceived -= OnStateReceived;
        _inputSystem.InputDeltaReceived -= OnInputReceived;
    }

    public void OnStateReceived(TapState tapState)
    {
        switch (tapState)
        {
            case TapState.Pressed:
                {
                    SetScopeState();
                    _gameplayUI.ShowScopeOverlay();
                    break;
                }
            case TapState.Released:
                {
                    SetDefaultState();
                    _gameplayUI.HideScopeOverlay();
                    break;
                }
        }
    }

    public void SetScopeState()
    {
        _mainCamera.enabled = false;
        _scopeCamera.enabled = true;
    }

    public void SetDefaultState()
    {
        _mainCamera.enabled = true;
        _scopeCamera.enabled = false;
    }

    private void OnInputReceived(Vector2 xyInput)
    {
        Vector2 newXYInput;
        if (_limitAngles)
        {
            newXYInput.x = CalculateNewAxisAngle(_scopeCamera.transform.rotation.eulerAngles.x, xyInput.x, _xMinAngle, _xMaxAngle);
            newXYInput.y = CalculateNewAxisAngle(_scopeCamera.transform.rotation.eulerAngles.y, xyInput.y, _yMinAngle, _yMaxAngle);
        }
        else
        {
            newXYInput.x = CalculateNewAxisAngle(_scopeCamera.transform.rotation.eulerAngles.x, xyInput.x);
            newXYInput.y = CalculateNewAxisAngle(_scopeCamera.transform.rotation.eulerAngles.y, xyInput.y);
        }

        _scopeCamera.transform.rotation = Quaternion.Euler(newXYInput.x, newXYInput.y, 0);
    }

    private float CalculateNewAxisAngle(float axisEulerAngle, float inputRotation)
    {
        return CalculateNewAxisAngle(axisEulerAngle, inputRotation, -360f, 360f);
    }

    private float CalculateNewAxisAngle(float axisEulerAngle, float inputRotation, float minAngle, float maxAngle)
    {
        float result = axisEulerAngle + inputRotation;
        result = result > 180 ? result - 360 : result;
        return Mathf.Clamp(result, minAngle, maxAngle);
    }
}