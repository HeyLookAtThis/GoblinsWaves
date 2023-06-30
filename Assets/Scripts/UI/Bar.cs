using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class Bar : MonoBehaviour
{
    [SerializeField] private Player _player;

    protected float Speed = 0.5f;
    protected Slider Slider;
    
    private Coroutine _volumeChangerCoroutine;

    public Player Player => _player;

    private void Awake()
    {
        Slider = GetComponent<Slider>();
    }

    protected void OnBeginChangeValue(float newValue)
    {
        if (_volumeChangerCoroutine != null)
            StopCoroutine(_volumeChangerCoroutine);

        _volumeChangerCoroutine = StartCoroutine(VolumeChanger(newValue));
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
