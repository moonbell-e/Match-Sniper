using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _shootEffect;
    public void CalculateBulletTarget(Camera camera)
    {
        Vector2 centerOfScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = camera.ScreenPointToRay(centerOfScreen);

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            if(hit.collider.TryGetComponent<Unit>(out var unit))
                unit.TakeDamage();
            Debug.Log($"Sniped {hit.collider.name}");
        }
    }
}
