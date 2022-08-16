using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExplosionHandler : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _explosions;
    private CameraShake _cameraShake;

    private void Awake()
    {
        _cameraShake = GetComponent<CameraShake>();    
    }

    private void Start()
    {
        InvokeRepeating("ShowExplosion", Random.Range(5f, 10f), Random.Range(4f, 8f));
    }
    //private void Update()
    //{
    //    InvokeRepeating("ShowExplosion", Random.Range(10f, 10f), Random.Range(30f, 120f));
    //}

    private void ShowExplosion()
    {
        _explosions[Random.Range(0, _explosions.Count - 1)].Play();
        _cameraShake.Shake();
    }
}
