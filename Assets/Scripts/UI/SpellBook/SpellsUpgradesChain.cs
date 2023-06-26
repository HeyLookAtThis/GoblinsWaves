using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SpellsUpgradesChain : MonoBehaviour
{
    [SerializeField] private SpellUpgradeButton _botton;
    [SerializeField] private GameObject _image;

    private List<SpellUpgradeButton> _buttons = new List<SpellUpgradeButton>();

    private Player _player;

    public void CreateButtons(Spell spell, WindowBuySpells spellInfo, Player player)
    {
        int spellLevel = spell.CurrentLevel;

        spell.InitializeLevelsDescriptions();

        _player = player;

        for (int i = 0; i < spell.Levels; i++)
        {
            SpellUpgradeButton button = Instantiate(_botton, transform);
            
            spellLevel += i;

            button.Initialize(spell, spellLevel, spell.ShowLevelDescription(spellLevel), spellInfo);
            _buttons.Add(button);

            if(spellLevel < spell.Levels)
                Instantiate(_image, transform);

            if (i > 0)
                button.SetButtonInteractable(false);
        }

        CheckPlayerSpells();
        OpenNextButton();
    }

    private void CheckPlayerSpells()
    {
        foreach(var button in _buttons)
            button.SetUpgrade(_player.CheckSpellLevel(button.Spell));
    }

    public void OpenNextButton()
    {
        for (int i = 0; i < _buttons.Count; i++)
            if (_buttons[i].IsUpgraded)
                if (_buttons[i + 1] != null)
                    _buttons[i + 1].SetButtonInteractable(true);
    }
}
