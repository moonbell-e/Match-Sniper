using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputSystem _inputSystem;
    [SerializeField] private GameplayUI _gameplayUI;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Camera _scopeCamera;
    [SerializeField] private Rifle _rifle;

    [SerializeField] private bool _limitAngles;
    [SerializeField] private float _xMinAngle;
    [SerializeField] private float _xMaxAngle;
    [SerializeField] private float _yMinAngle;
    [SerializeField] private float _yMaxAngle;

    public Camera ScopeCamera => _scopeCamera;

    private void Awake()
    {
        _mainCamera.enabled = true;
        _inputSystem.InputDeltaReceived += OnInputReceived;
    }

    private void OnDestroy()
    {
        _inputSystem.InputDeltaReceived -= OnInputReceived;
    }


    private void OnInputReceived(Vector2 inputDelta)
    {
        Vector2 dnewInputDelta;
        if (_limitAngles)
        {
            dnewInputDelta.x = CalculateNewAxisAngle(_scopeCamera.transform.rotation.eulerAngles.x, inputDelta.x, _xMinAngle, _xMaxAngle);
            dnewInputDelta.y = CalculateNewAxisAngle(_scopeCamera.transform.rotation.eulerAngles.y, inputDelta.y, _yMinAngle, _yMaxAngle);
        }
        else
        {
            dnewInputDelta.x = CalculateNewAxisAngle(_scopeCamera.transform.rotation.eulerAngles.x, inputDelta.x);
            dnewInputDelta.y = CalculateNewAxisAngle(_scopeCamera.transform.rotation.eulerAngles.y, inputDelta.y);
        }

        _scopeCamera.transform.rotation = Quaternion.Euler(dnewInputDelta.x, dnewInputDelta.y, 0);
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