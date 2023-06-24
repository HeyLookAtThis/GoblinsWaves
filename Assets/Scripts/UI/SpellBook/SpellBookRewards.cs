using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpellBookRewards : MonoBehaviour
{
    [SerializeField] private CanvasController _canvas;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _text.text = _canvas.Player.Rewards.ToString();
    }
}
