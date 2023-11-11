using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public int currentLevel = 1;

    public int levelUpScale = 50;
    public int scoreUntilLevelUp = 100;
    public int prevLevels = 0;

    public void CheckForLevelUp()
    {
        int score = GameManager.gm.ui.score - prevLevels;

        if(score >= scoreUntilLevelUp)
        {
            IncreaseLevel();
        }
    }

    public void IncreaseLevel()
    {
        currentLevel++;

        prevLevels = scoreUntilLevelUp;
        scoreUntilLevelUp += levelUpScale;

        GameManager.gm.ui.ActivateUpgradeScreen(true);
    }

    public void ResetLevel()
    {
        currentLevel = 1;
        scoreUntilLevelUp = 100;
    }
}
