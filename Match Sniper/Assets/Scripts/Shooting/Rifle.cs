using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour, IInputReceivable
{
    [SerializeField] private ParticleSystem _shootEffect;
    [SerializeField] private RifleClip _rifleClip;
    [SerializeField] private InputSystem _inputSystem;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private List<Unit> _match;
    [SerializeField] private List<Unit> _prevMatch;

    private void OnEnable()
    {
        _inputSystem.InputReceived += OnStateReceived;
    }

    private void OnDisable()
    {
        _inputSystem.InputReceived -= OnStateReceived;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _animator.SetTrigger("Idle2");
    }

    public void OnStateReceived(TapState tapState)
    {
        if (tapState == TapState.Pressed)
        {
            ShowScopeOverlay();
        }
        if (tapState == TapState.Released)
        {
            HideScopeOverlay();
        }
    }

    public void ShowScopeOverlay()
    {
        _animator.SetBool("IsScope", true);
    }

    public void HideScopeOverlay()
    {
        _animator.SetBool("IsScope", false);
    }

    private bool CheckBulletsAmount()
    {
        if (_rifleClip.Bullets.Count > 0)
            return true;
        return false;
    }

    public void ShowMatch(Camera camera)
    {
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hit, 200f))
        {
            if (hit.collider.TryGetComponent(out Unit unit))
            {
                if (unit._matchs.Count <= 1)
                {
                    return;
                }
                else
                {
                    Debug.Log("match");
                    unit.OutlineSubstrate();
                    _match = unit._matchs;
                }
            }
            else
            {
                if (_match.Count > 0)
                    _match[0].DisableOutlineSubstrate();
            }
        }
    }

    public void TryShoot(Camera camera)
    {
        if (CheckBulletsAmount())
        {
            Bullet lastBullet = _rifleClip.GetLastBullet();

            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hit, 200f))
            {
                if (hit.collider.TryGetComponent(out Unit unit))
                {
                    Shoot(lastBullet, unit, hit);
                }
            }
        }
        else
        {
            Debug.Log("No bullets!");
        }
    }

    public void Shoot(Bullet lastBullet, Unit unit, RaycastHit hit)
    {
        lastBullet.transform.position = Vector3.MoveTowards(_bulletSpawnPoint.transform.position, hit.point, 100f);
        unit.TakeDamage(lastBullet.BulletDamage);
        unit.DamageMatch(lastBullet.BulletDamage);
        _rifleClip.RemoveLastBullet();
        ShowShoot();
        Debug.Log($"Sniped {hit.collider.name}");
    }
    private void ShowShoot()
    {
        _animator.SetTrigger("Shoot");
        _shootEffect?.Play();
    }
}
