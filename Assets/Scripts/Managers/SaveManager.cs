using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public int totalLevels = 5;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void SaveLevelProgress(int levelNumber, int stars)
    {
        string key = "Level_" + levelNumber + "_Stars";

        int oldStars = PlayerPrefs.GetInt(key, 0);
        if (stars > oldStars)
        { 
            PlayerPrefs.SetInt(key, stars);
            PlayerPrefs.Save();
        }
    }

    public int GetLevelStars(int levelNumber)
    {
        string key = "Level_" + levelNumber + "_Stars";
        int stars = PlayerPrefs.GetInt(key, 0);
        return stars;
    }

    public bool IsLevelUnlocked(int levelNumber)
    {
        if (levelNumber == 1)
            return true;
        string key = "Level_" + levelNumber + "_Unlocked";
        int unlocked = PlayerPrefs.GetInt(key, 0);

        if (unlocked == 1)
            return true;
        else
            return false;
    }

    public int GetTotalStars()
    {
        int total = 0;
        for (int i = 1; i <= totalLevels; i++)
        {
            int stars = GetLevelStars(i);
            total += stars;
        }
        return total;
    }
    
    public void UnlockLevel(int levelNumber)
    {
        int nextLevel = GameManager.instance.currentLevel + 1;
        if(nextLevel <= totalLevels)
        {
            string key = "Level_" + nextLevel + "_Unlocked";
            PlayerPrefs.SetInt(key, 1);
            PlayerPrefs.Save();
        }
    }
}
