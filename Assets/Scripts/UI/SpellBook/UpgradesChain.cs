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

    public void Create(Spell spell, WindowBuySpells spellInfo, Player player)
    {
        spell.InitializeLevelsDescriptions();

        _player = player;

        for (int i = 1; i <= spell.Levels; i++)
        {
            UpgradeButton button = Instantiate(_button, transform);            

            button.Initialize(spell, i, spell.ShowLevelDescription(i), spellInfo);

            if (i < spell.Levels)
                Instantiate(_image, transform);

            button.SetButtonInteractable(false);

            button.SetUpgrade(_player.CheckSpell(spell, i));

            _buttons.Add(button);
        }

        OpenNextButton();
    }

    public void OpenNextButton()
    {
        for (int i = 0; i < _buttons.Count; i++)
            if (_buttons[i].IsUpgraded)
                if (_buttons[i + 1] != null)
                    _buttons[i + 1].SetButtonInteractable(true);
    }
}
