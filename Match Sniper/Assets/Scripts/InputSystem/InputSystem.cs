using System;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public event Action<TapState> InputReceived;
    public event Action<Vector2> InputDeltaReceived;

    [SerializeField] private float _horizontalSensitivity;
    [SerializeField] private float _verticalSensitivity;
    [SerializeField] private float _touchHorizontalSensitivity;
    [SerializeField] private float _touchVerticalSensitivity;

    [Header("Debug")]
    [SerializeField] private bool _enabled;
    [SerializeField] private TapState _tapState;

    private void Awake()
    {
        _tapState = TapState.None;
    }

    private void Update()
    {
        OnButtonHeld();
        OnButtonUp();
    }

    public void Enable()
    {
        _enabled = true;
    }

    public void Disable()
    {
        _enabled = false;
    }

    private void OnButtonUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (_tapState == TapState.Pressed)
            {
                _tapState = TapState.Released;
                InputReceived?.Invoke(_tapState);
            }

            _tapState = TapState.None;
        }
    }

    private void OnButtonHeld()
    {
        if (Input.GetMouseButton(0))
        {
            _tapState = TapState.Pressed;
            InputReceived?.Invoke(_tapState);
            SendInputDelta();
        }
    }

    private void SendInputDelta()
    {
        Vector2 inputDelta;
        inputDelta.x = -Input.GetAxis("Mouse Y") * _horizontalSensitivity;
        inputDelta.y = Input.GetAxis("Mouse X") * _verticalSensitivity;

        float xInput = -Input.GetAxis("Mouse Y") * _horizontalSensitivity;
        float yInput = Input.GetAxis("Mouse X") * _verticalSensitivity;

        if (Input.touchCount > 0)
        {
            inputDelta.x = -Input.touches[0].deltaPosition.y / Screen.height * _touchHorizontalSensitivity;
            inputDelta.y = Input.touches[0].deltaPosition.x / Screen.width * _touchVerticalSensitivity;
        }

        InputDeltaReceived?.Invoke(inputDelta);
    }
}
