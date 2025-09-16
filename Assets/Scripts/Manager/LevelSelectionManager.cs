using System;
using UnityEngine;

public class LevelSelectionManager : MonoBehaviour
{
    private int maxLvl = 10;
    private int unlockedLevels = 0;

    private void Awake()
    {
        FlagPole.OnUnlockLevel += UnlockLevel;
        Nam.OnUnlockLevel += UnlockLevel;
        Princess.OnUnlockLevel += UnlockLevel;
    }

    private void UnlockLevel(int lvl)
    {
        unlockedLevels = PlayerPrefs.GetInt("UnlockedLvl", 0);
        if (lvl > maxLvl)
        {
            unlockedLevels = maxLvl;
        }
        if (lvl >= unlockedLevels)
        {
            unlockedLevels = lvl;
        }
        PlayerPrefs.SetInt("UnlockedLvl", unlockedLevels);
    }

    public void LoadLevel(int level)
    {
        GameManager.Instance.lives = 3;
        GameManager.Instance.coins = 0;
        GameManager.Instance.LoadLevel(level);
    }
}
