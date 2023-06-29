using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UpgradesChain : MonoBehaviour
{
    [SerializeField] private UpgradeButton _button;
    [SerializeField] private GameObject _image;

    private List<UpgradeButton> _buttons = new List<UpgradeButton>();

    private Player _player;

    public void Create(Spell spell, DescriptionPanel descriptionPanel, Player player)
    {
        spell.InitializeLevelsDescriptions();

        _player = player;

        for (int i = 1; i <= spell.Levels; i++)
        {
            UpgradeButton button = Instantiate(_button, transform);            

            button.Initialize(spell, i, spell.ShowLevelDescription(i), descriptionPanel);

            if (i < spell.Levels)
                Instantiate(_image, transform);

            button.SetButtonInteractable(false);

            button.OnUpgraded += OpenNextButton;

            button.SetUpgrade(_player.CheckSpell(spell, i));

            _buttons.Add(button);
        }
    }

    private void OpenNextButton(UpgradeButton button)
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            if (_buttons[i].IsUpgraded)
            {
                _buttons[i].OnUpgraded -= OpenNextButton;

                if (i + 1 < _buttons.Count)
                {
                    _buttons[i + 1].SetButtonInteractable(true);
                }
            }
        }
    }
}
