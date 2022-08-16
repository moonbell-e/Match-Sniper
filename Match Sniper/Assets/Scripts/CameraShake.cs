using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    #region Singleton Init
    private static CameraShake _instance;

    void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
        {
            Debug.Log($"Destroying {gameObject.name}, caused by one singleton instance");
            Destroy(gameObject);
        }
    }

    public static CameraShake Instance // Init not in order
    {
        get
        {
            if (_instance == null)
                Init();
            return _instance;
        }
        private set { _instance = value; }
    }

    static void Init() // Init script
    {
        _instance = FindObjectOfType<CameraShake>();
        if (_instance != null)
            _instance.Initialize();
    }
    #endregion

    [Header("Data to change")]
    public List<Transform> targets;
    public float duration;
    public float magnitude;
    public float constantMagnitude;

    [Header("Internal")]
    public bool isDoingConstantShake = false;
    public bool isShaking = false;
    public List<Vector3> origPositions;
    public float constantPercentAmplitude;

    void Initialize()
    {
        // Init data here
        enabled = true;
    }

    private void Start()
    {
        origPositions.Clear();
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null)
                origPositions.Add(targets[i].localPosition);
        }
    }

    //private void LateUpdate()
    //{
    //    if (isShaking)
    //        return;

    //    if (MainData.Instance.isBroken || GameManager.Instance.gameState != GameManager.GameState.PlayingGame)
    //    {
    //        if (isDoingConstantShake)
    //        {
    //            for (int i = 0; i < targets.Count; i++)
    //            {
    //                targets[i].localPosition = new Vector3(origPositions[i].x, origPositions[i].y, origPositions[i].z);
    //            }
    //        }
    //        return;
    //    }


    //    if (Minigame.Instance.isPlaying)
    //    {
    //        if (isDoingConstantShake)
    //        {
    //            for (int i = 0; i < targets.Count; i++)
    //            {
    //                targets[i].localPosition = new Vector3(origPositions[i].x, origPositions[i].y, origPositions[i].z);
    //            }
    //        }
    //        return;
    //    }

    //    if (MainData.Instance.engineTemperature > MainData.Instance.engineWarningTemperature)
    //    {
    //        var overflow = MainData.Instance.engineTemperature - MainData.Instance.engineWarningTemperature;
    //        if (overflow > 0f)
    //        {
    //            if (!isDoingConstantShake)
    //            {
    //                origPositions.Clear();
    //                for (int i = 0; i < targets.Count; i++)
    //                {
    //                    origPositions.Add(targets[i].localPosition);
    //                }
    //            }

    //            var range = MainData.Instance.engineWarningMaxTemperature - MainData.Instance.engineWarningTemperature;
    //            constantPercentAmplitude = overflow / range;

    //            var rand = Random.insideUnitCircle;
    //            float x = rand.x * constantMagnitude * constantPercentAmplitude;
    //            float y = rand.y * constantMagnitude * constantPercentAmplitude;

    //            for (int i = 0; i < targets.Count; i++)
    //            {
    //                targets[i].localPosition = new Vector3(origPositions[i].x + x, origPositions[i].y + y, origPositions[i].z);
    //            }

    //            isDoingConstantShake = true;
    //        }
    //    }
    //}
    
    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }
    public IEnumerator ShakeCoroutine()
    {
        if (isShaking)
            yield break;

        if (!isDoingConstantShake)
        {
            origPositions.Clear();
            for (int i = 0; i < targets.Count; i++)
            {
                origPositions.Add(targets[i].localPosition);
            }
        }

        isShaking = true;
        
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            var rand = Random.insideUnitCircle;
            float x = rand.x * magnitude * ((duration - elapsed) / duration);
            float y = rand.y * magnitude * ((duration - elapsed) / duration);

            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].localPosition = 
                    new Vector3(
                        origPositions[i].x + x, 
                        origPositions[i].y + y, 
                        origPositions[i].z);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }
        for (int i = 0; i < targets.Count; i++)
        {
            targets[i].localPosition = origPositions[i];
        }
        isShaking = false;
    }
}
