using System.Collections;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _shootEffect;
    [SerializeField] private RifleClip _rifleClip;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _bulletSpawnPoint;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private  void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _animator.SetTrigger("Idle2");
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

    public void TryShoot(Camera camera)
    {
        if (CheckBulletsAmount())
        {
            Bullet lastBullet = _rifleClip.GetLastBullet();

            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hit, 200f))
            {
                if (hit.collider.TryGetComponent<IDamageable>(out var unit))
                {
                    lastBullet.transform.position = Vector3.MoveTowards(_bulletSpawnPoint.transform.position, hit.point, 100f);

                    unit.TakeDamage(lastBullet.BulletDamage);
                    _rifleClip.RemoveLastBullet();
                    ShowShoot();
                    Debug.Log($"Sniped {hit.collider.name}");
                }
            }
        }
        else
        {
            Debug.Log("No bullets!");
        }
    }

    private void ShowShoot()
    {
        _animator.SetTrigger("Shoot");
        _shootEffect?.Play();
    }
}
