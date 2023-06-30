using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpellBookRewards : MonoBehaviour
{
    [SerializeField] private CanvasController _canvas;
    
    private TMP_Text _text;
    private Player _player;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _player = _canvas.Player;
    }

    private void OnEnable()
    {
        _player.ChangedRewards += OnShowRewards;
        _text.text = _player.Rewards.ToString();
    }

    private void OnDisable()
    {
        _player.ChangedRewards -= OnShowRewards;
    }

    private void Start()
    {
        OnShowRewards(_player.Rewards);
    }

    private void OnShowRewards(int rewards)
    {
        _text.text = rewards.ToString();
    }
}
