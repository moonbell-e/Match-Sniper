using UnityEngine;

public class Unit : MonoBehaviour, IDamageable
{
    [SerializeField] private UnitDataSO _unitData;
    [SerializeField] private UnitColour _colour;
    [SerializeField] private UnitType _type;
    [SerializeField] private int _health;

    private void Awake()
    {
        Init(_unitData);
    }
    
    public void Init(UnitDataSO unitData)
    {
        _type = unitData.Type;
        _health = unitData.Health;
    }

    public void TakeDamage(int damageValue)
    {
        if (_health >= 2)
            _health -= damageValue;
        else
            Destroy(gameObject);
    }
}
