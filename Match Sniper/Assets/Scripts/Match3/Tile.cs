using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool _isOccupied;
    public bool IsOccupied => _isOccupied;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            Debug.Log("Occupied!");
            _isOccupied = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            Debug.Log("Not occupied!");
            _isOccupied = false;
        }
    }

    
}
