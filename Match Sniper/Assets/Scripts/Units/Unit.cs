using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IDamageable
{
    [SerializeField] private UnitDataSO _unitData;
    [SerializeField] private UnitType _type;
    [SerializeField] protected int _health;
    [SerializeField] private MeshRenderer _substrate;
    [SerializeField] private float _deathAnimDuration;
    public List<Unit> _matchs;
    private bool _isOutlined;
    private bool _isDisabled;

    public List<Unit> Matchs => _matchs;

    protected Animator _animator;

    public UnitType Type => _type;

    private void Awake()
    {
        Init(_unitData);
        _animator = GetComponent<Animator>();
    }

    public void OutlineSubstrate()
    {
        _isDisabled = false;
        if (_matchs.Count <= 1)
        {
            return;
        }
        else
        {
            if (_isOutlined)
                return;
            _isOutlined = true;
            Color color = _substrate.material.color;
            _substrate.material.EnableKeyword("_EMISSION");
            _substrate.material.SetColor("_EmissionColor", color * 1f);
            foreach (var unit in _matchs)
            {
                unit.OutlineSubstrate();
            }
        }
    }

    public void DisableOutlineSubstrate()
    {
        _isOutlined = false;
        if (_matchs.Count <= 1)
        {
            return;
        }
        else
        {
            if (_isDisabled)
                return;
            _isDisabled = true;
            Color color = _substrate.material.color;
            _substrate.material.EnableKeyword("_EMISSION");
            _substrate.material.SetColor("_EmissionColor", color / 5f);
            foreach (var unit in _matchs)
            {
                unit.DisableOutlineSubstrate();
            }
        }
    }

    public void DamageMatch(int value)
    {
        foreach (var unit in _matchs)
        {
            unit.TakeDamage(value);
        }
    }

    public void Init(UnitDataSO unitData)
    {
        _type = unitData.Type;
        _health = unitData.Health;
    }

    public virtual void TakeDamage(int damageValue)
    {
        if (_health >= 2)
        {
            _health -= damageValue;
        }
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
