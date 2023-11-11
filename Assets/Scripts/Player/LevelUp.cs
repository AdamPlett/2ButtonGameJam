using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public int currentLevel = 1;

    public int levelUpScale;
    public int scoreUntilLevelUp;
    public int scoreForLastLevel;

    public void CheckForLevelUp()
    {
        int scoreToCheck = GameManager.gm.ui.score - scoreForLastLevel;

        if(scoreToCheck >= scoreUntilLevelUp)
        {
            IncreaseLevel();
        }
    }

    public void IncreaseLevel()
    {
        currentLevel++;

        scoreForLastLevel = scoreUntilLevelUp;

        scoreUntilLevelUp = GameManager.gm.ui.score + levelUpScale;

        GameManager.gm.ui.ActivateUpgradeScreen(true);
    }

    public void ResetLevel()
    {
        currentLevel = 1;
        scoreUntilLevelUp = 100;
        scoreForLastLevel = 0;
    }
}
