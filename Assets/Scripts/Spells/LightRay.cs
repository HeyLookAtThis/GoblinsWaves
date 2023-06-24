using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRay : Spell
{
    private float _manaCostForUpgrade;

    private void Start()
    {
        _manaCostForUpgrade = manaCost;
        InitializeLevelsDescriptions();

        upgradeCost = 25;
        levels = 3;
    }

    public override void Upgrade()
    {
        manaCost -= _manaCostForUpgrade;
    }

    public override void InitializeLevelsDescriptions()
    {
        string firstLevelDescription = "Заклинание начала уровня";
        string secondLevelDescription = "Не затрачивает ману";
        string thirdLevelDescription = "Восстанавливает ману";

        int firsLevel = 1;
        int secondLevel = 2;
        int thirdLevel = 3;

        levelsDescriptions.Add(firsLevel, firstLevelDescription);
        levelsDescriptions.Add(secondLevel, secondLevelDescription);
        levelsDescriptions.Add(thirdLevel, thirdLevelDescription);
    }

    public override string ShowLevelDescription(int level)
    {
        return levelsDescriptions[level];
    }
}
