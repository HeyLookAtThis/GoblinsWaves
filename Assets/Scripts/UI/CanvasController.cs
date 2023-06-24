using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Button _callButton;
    [SerializeField] private Player _player;
    [SerializeField] private Button _closeButton;

    public Player Player => _player;

    public void OnCallButtonClick(GameObject panel)
    {
        panel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void OnCloseButtonClick(GameObject panel)
    {
        panel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
