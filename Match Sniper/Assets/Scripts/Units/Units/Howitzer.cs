using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Howitzer : Unit
{
    [Header("Colour Settings")]
    [SerializeField] private MeshRenderer _body;
    [SerializeField] private ParticleSystem _explosionEffect;

    public MeshRenderer Body => _body;

    public override IEnumerator WaitAndKill()
    {
        _explosionEffect.Play();
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
