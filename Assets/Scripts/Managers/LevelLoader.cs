using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        if (nextIndex < totalScenes && nextIndex >=2)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
            Debug.Log("No more levels to load.");
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }

    public void LoadLevel(int levelIndex)
    {
        Time.timeScale = 1f;
        if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            Debug.LogError("Level index out of range: " + levelIndex);
        }
    }
}
