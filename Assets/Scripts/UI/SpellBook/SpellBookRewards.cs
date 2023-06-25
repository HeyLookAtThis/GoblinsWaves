using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpellBookRewards : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _text.text = _player.Rewards.ToString();
    }
}
