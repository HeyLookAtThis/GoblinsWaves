using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class RewardsBoard : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Text _board;

    private Coroutine _textChanger;
    private int _rewardsValue;

    private void OnEnable()
    {
        _player.OnChangedRewards += BeginChangeText;
    }

    private void OnDisable()
    {
        _player.OnChangedRewards += BeginChangeText;
    }

    private void Start()
    {
        _board = GetComponent<Text>();
        _board.text = _player.Rewards.ToString();
    }

    private void BeginChangeText(int targetValue)
    {
        if (_textChanger != null)
            StopCoroutine(_textChanger);

        _textChanger = StartCoroutine(TextChanger(targetValue));
    }

    private IEnumerator TextChanger(int targetValue)
    {
        float seconds = 0.1f;
        var waitTime = new WaitForSeconds(seconds);

        while (true)
        {
            if(_board.text != targetValue.ToString())
            {
                int.TryParse(_board.text, out _rewardsValue);
                _rewardsValue++;
                _board.text = _rewardsValue.ToString();
                yield return waitTime;
            }
            else
            {
                yield break;
            }
        }
    }
}
