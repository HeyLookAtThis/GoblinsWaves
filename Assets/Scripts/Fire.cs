using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Fire))]
public class Fire : MonoBehaviour
{
    private Light _light;

    private void Awake()
    {
        _light = GetComponent<Light>();
    }

    private void OnEnable()
    {
        StartCoroutine(IntensityChanger());
    }

    private void OnDisable()
    {
        StopCoroutine(IntensityChanger());
    }

    private IEnumerator IntensityChanger()
    {
        float minIntensity = 2.7f;
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
