using UnityEngine;

public class Rifle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _shootEffect;
    [SerializeField] private RifleClip _rifleClip;
    public void ShootUnit(Camera camera)
    {
        Ray ray = GetScreenPointToRay(camera);
        TryShoot(ray);
    }

    private Ray GetScreenPointToRay(Camera camera)
    {
        Vector2 centerOfScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = camera.ScreenPointToRay(centerOfScreen);
        return ray;
    }
    private bool CheckBulletsAmount()
    {
        if (_rifleClip.Bullets.Count > 0)
            return true;
        return false;
    }

    private void TryShoot(Ray ray)
    {
        if (CheckBulletsAmount())
        {
            int lastBulletsDamage = _rifleClip.GetLastBullet().BulletDamage;

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<IDamageable>(out var unit))
                    unit.TakeDamage(lastBulletsDamage);

                _rifleClip.RemoveLastBullet();
                Debug.Log($"Sniped {hit.collider.name}");
                Debug.Log(hit.collider.transform.position);
            }
        }
        else
        {
            Debug.Log("No bullets!");
        }
    }
}
