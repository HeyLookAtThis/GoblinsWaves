using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SpellsUpgradesChain : MonoBehaviour
{
    [SerializeField] private SpellUpgrade _botton;
    [SerializeField] private GameObject _image;

    private List<SpellUpgrade> _buttons = new List<SpellUpgrade>();    

    public void CreateButtons(Spell spell, SpellInfoView spellInfo, Player player)
    {
        int spellListNumber = 1;

        spell.InitializeLevelsDescriptions();

        for (int i = 0; i < spell.Levels; i++)
        {
            SpellUpgrade button = Instantiate(_botton, transform);
            
            spellListNumber += i;

            button.Initialize(spell, spellListNumber, spell.ShowLevelDescription(spellListNumber), spellInfo, player);
            _buttons.Add(button);

            if(spellListNumber < spell.Levels)
                Instantiate(_image, transform);

            if (i > 0)
                button.SetButtonInteractable(false);

            if (spellListNumber == spell.CurrentLevel)
                player.UpgradeSpell(spell, spell.UpgradeCost);

        }

        OpenNextButton();
    }

    private void OpenNextButton()
    {
        for (int i = 0; i < _buttons.Count; i++)
            if (_buttons[i].IsUpgraded)
                if (_buttons[i + 1] != null)
                    _buttons[i + 1].SetButtonInteractable(true);
    }
}
