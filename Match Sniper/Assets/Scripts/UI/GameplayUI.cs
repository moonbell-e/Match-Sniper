using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private GameObject _scopeOverlay;

    public void ShowScopeOverlay()
    {
        StartCoroutine(WaitAnimDelay());
        _scopeOverlay.SetActive(true);
    }
    
    public void HideScopeOverlay()
    {
        _scopeOverlay.SetActive(false);
    }

    private IEnumerator WaitAnimDelay()
    {
        yield return new WaitForSeconds(1.5f);
    }
}
