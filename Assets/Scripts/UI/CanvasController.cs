using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class CanvasController : MonoBehaviour
{
    [SerializeField] private Button _callButton;
    [SerializeField] private Player _player;
    [SerializeField] private Button _closeButton;
    [SerializeField] private AudioClip _closeSound;
    [SerializeField] private AudioClip _opendSound;

    public Player Player => _player;

    private AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public void OnCallButtonClick(GameObject panel)
    {
        _source.PlayOneShot(_opendSound);
        panel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void OnCloseButtonClick(GameObject panel)
    {
        _source.PlayOneShot(_closeSound);
        panel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
