using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour, IDamageable
{
    [SerializeField] private UnitDataSO _unitData;
    [SerializeField] private UnitColour _colour;
    [SerializeField] private UnitType _type;
    [SerializeField] protected int _health;
    [SerializeField] private int _gridSize;
    [SerializeField] private float _deathAnimDuration;
    protected Animator _animator;

    public UnitColour Colour => _colour;
    public UnitType Type => _type;

    private void Awake()
    {
        Init(_unitData);
        _animator = GetComponent<Animator>();
    }
    
    public void Init(UnitDataSO unitData)
    {
        _type = unitData.Type;
        _health = unitData.Health;
        _gridSize = unitData.GridSize;
    }

    public virtual void TakeDamage(int damageValue)
    {
        if (_health >= 2)
            _health -= damageValue;
        else
        {
            _animator.SetTrigger("IsDead");
            StartCoroutine(WaitAndKill());
        }
    }

    public virtual IEnumerator WaitAndKill()
    {
        yield return new WaitForSeconds(_deathAnimDuration);
        Destroy(gameObject);
    }
}
