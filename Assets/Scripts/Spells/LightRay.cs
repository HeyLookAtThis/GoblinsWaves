using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LightRay : Spell
{
    private float _manaCostForUpgrade;    

    private void Start()
    {
        _manaCostForUpgrade = manaCost;
    }

    public override void SetLevel(int level)
    {
        currentLevel = level;
        manaCost -= _manaCostForUpgrade * --level;
    }

    public override void InitializeLevelsDescriptions()
    {
        string firstLevelDescription = "Заклинание начала игры";
        string secondLevelDescription = "Не затрачивает очки маны";
        string thirdLevelDescription = "Восстанавливает очки маны";

        int firsLevel = 1;
        int secondLevel = 2;
        int thirdLevel = 3;

        levelsDescriptions.Add(firsLevel, firstLevelDescription);
        levelsDescriptions.Add(secondLevel, secondLevelDescription);
        levelsDescriptions.Add(thirdLevel, thirdLevelDescription);
    }

    public override string ShowLevelDescription(int level)
    {
        levelsDescriptions.TryGetValue(level, out var levelDescription);
        return levelDescription;
    }
}
