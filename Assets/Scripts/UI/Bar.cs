using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected Player Player;

    protected Slider Slider;
    protected float Speed = 0.5f;

    private Coroutine _volumeChanger;

    private void Awake()
    {
        Slider = GetComponent<Slider>();
    }

    protected void BeginChangeValue(float newValue)
    {
        if (_volumeChanger != null)
            StopCoroutine(_volumeChanger);

        _volumeChanger = StartCoroutine(VolumeChanger(newValue));
    }

    private IEnumerator VolumeChanger(float newValue)
    {
        float seconds = 0.01f;
        var waitTime = new WaitForSeconds(seconds);

        while (newValue != Slider.value)
        {
            Slider.value = Mathf.MoveTowards(Slider.value, newValue, Speed);
            yield return waitTime;
        }

        if (newValue == Slider.value)
            yield break;
    }
}
