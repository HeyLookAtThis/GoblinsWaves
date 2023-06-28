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
        _player.OnChangedRewards += ShowRewards;
        _text.text = _player.Rewards.ToString();
    }

    private void OnDisable()
    {
        _player.OnChangedRewards -= ShowRewards;
    }

    private void Start()
    {
        ShowRewards(_player.Rewards);
    }

    private void ShowRewards(int rewards)
    {
        _text.text = rewards.ToString();
    }
}
