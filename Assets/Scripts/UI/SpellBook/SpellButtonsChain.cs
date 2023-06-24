using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellButtonsChain : MonoBehaviour
{
    [SerializeField] private SpellButton _botton;

    private List<SpellButton> _buttons = new List<SpellButton>();    

    public void Create(Spell spell)
    {
        int spellListNumber = 1;

        for (int i = 0; i < spell.Levels; i++)
        {
            SpellButton button = Instantiate(_botton, transform);
            
            spellListNumber += i;

            button.Initialize(spell, spellListNumber, spell.ShowLevelDescription(spellListNumber));
            _buttons.Add(button);

            if (i > 0)
                button.SetButtonInteractable(false);
        }
    }
}
