using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSystem : MonoBehaviour
{
    [SerializeField] private List<Bullet> _bullets;
    public List<Bullet> Bullets => _bullets;
    public Bullet GetLastBullet()
    {
        if (_bullets.Count == 1)
            return _bullets[0];
        else
            return _bullets[_bullets.Count - 2];
    }
} 
