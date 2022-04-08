using System.Collections.Generic;
using UnityEngine;

public class RifleClip : MonoBehaviour
{
    [SerializeField] private List<Bullet> _bullets;

    public IReadOnlyList<Bullet> Bullets => _bullets;

    public void RemoveLastBullet()
    {
        _bullets.Remove(GetLastBullet());
    }

    public Bullet GetLastBullet()
    {
        return _bullets[_bullets.Count - 1];
    }
}
