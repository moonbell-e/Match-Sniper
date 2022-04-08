using System;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public event Action<GunState> InputReceived;
    public event Action<float, float> InputDeltaReceived;

    [SerializeField] private float _horizontalSensitivity;
    [SerializeField] private float _verticalSensitivity;
    [SerializeField] private float _touchHorizontalSensitivity;
    [SerializeField] private float _touchVerticalSensitivity;

    [Header("Debug")]
    [SerializeField] private bool _enabled;
    [SerializeField] private GunState _gunState;

    public void Enable()
    {
        _enabled = true;
    }

    public void Disable()
    {
        _enabled = false;
    }

    private void Update()
    {
        OnButtonDown();
        OnButtonUp();
    }

    private void OnButtonUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (_gunState == GunState.Aimed)
            {
                _gunState = GunState.Shoot;
                InputReceived?.Invoke(_gunState);
            }

            _gunState = GunState.None;
        }
    }
    private void OnButtonDown()
    {
        if (Input.GetMouseButton(0))
        {
            _gunState = GunState.Aimed;
            InputReceived?.Invoke(_gunState);
            SendInputDelta();
        }
    }

    private void SendInputDelta()
    {
        float xInput = -Input.GetAxis("Mouse Y") * _horizontalSensitivity;
        float yInput = Input.GetAxis("Mouse X") * _verticalSensitivity;

        if (Input.touchCount > 0)
        {
            xInput = -Input.touches[0].deltaPosition.y / Screen.height * _touchHorizontalSensitivity;
            yInput = Input.touches[0].deltaPosition.x / Screen.width * _touchVerticalSensitivity;
        }

        InputDeltaReceived?.Invoke(xInput, yInput);
    }
}
