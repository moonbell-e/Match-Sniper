using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _unitSpeed;

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _unitSpeed);
    }
}
