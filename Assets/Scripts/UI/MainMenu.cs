using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        int lastUnlockedLevel = 1;

        if (SaveManager.instance != null)
        {
            for (int i = 1; i <= 5; i++)
            {
                if (SaveManager.instance.IsLevelUnlocked(i))
                {
                    lastUnlockedLevel = i;
                }
                else
                {
                    break;
                }
            }
        }
        int sceneIndex = lastUnlockedLevel + 1;
        SceneManager.LoadScene(sceneIndex);
    }

    public void OpenLevelSelector()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    public void OpenShop()
    {
        SceneManager.LoadScene("ContainerShop");
    }
}