using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHUD : MonoBehaviour
{
    public static GameHUD instance;
    public Slider waterSlider;
    public Button nextLevelButton;
    public GameObject endPanel;
void Awake()
{
    if (instance == null)
        instance = this;
    else
        Destroy(gameObject);
}
    public void ShowEndPanel()
    {
        endPanel.SetActive(true);
    }

    public void UpdateWaterUI(float waterPercentage)
    {
        if (waterSlider != null)
        {
            waterSlider.value = waterPercentage * 100f;
        }
        
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
