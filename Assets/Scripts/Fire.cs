using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class Fire : MonoBehaviour
{
    private Light _light;
    private Coroutine _intensityChangerCoroutine;

    private void Awake()
    {
        _light = GetComponent<Light>();
    }

    private void OnEnable()
    {
        BeginChangeIntensity();
    }

    private void OnDisable()
    {
        EndChangeIntencity();
    }

    private void BeginChangeIntensity()
    {
        if (_intensityChangerCoroutine != null)
            StopCoroutine(_intensityChangerCoroutine);

        _intensityChangerCoroutine = StartCoroutine(IntensityChanger());
    }

    private void EndChangeIntencity()
    {
        if (_intensityChangerCoroutine != null)
            StopCoroutine(_intensityChangerCoroutine);
    }

    private IEnumerator IntensityChanger()
    {
        float minIntensity = 2.5f;
        float maxIntensity = 3;

        float seconds = 0.1f;

        var waitTime = new WaitForSeconds(seconds);

        while (true)
        {
            _light.intensity = Random.Range(minIntensity, maxIntensity);
            yield return waitTime;
        }
    }
}
