using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        int lastUnlockedLevel = 1;
        for (int i = 1; i <= 5; i++)
        {
            if(SaveManager.instance.IsLevelUnlocked(i))
            {
                lastUnlockedLevel = i;
            }
            else
            {
                break;
            }
        }
        Debug.Log("Last Unlocked Level: " + lastUnlockedLevel);
        int sceneIndex = lastUnlockedLevel + 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

    public void OpenLevelSelector()
    {
        Debug.Log("Level Selector Opened");
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelector");                                                                                                            
    }
}
