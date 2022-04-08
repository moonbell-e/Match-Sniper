using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    { 
        Default,
        Explosive
    }

    [SerializeField] private int _bulletDamage;
    [SerializeField] private BulletType _type;

    //[SerializeField] private ParticleSystem _hitVFX;

    public int BulletDamage => _bulletDamage;
    public BulletType Type => _type;
}
