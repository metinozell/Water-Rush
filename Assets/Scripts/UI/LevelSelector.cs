using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public GameObject levelButtonPrefab;
    public Transform contentPanel;
    public int totalLevels = 5;
    private SaveManager saveManager;

    void Start()
    {
        Debug.Log("LevelSelector Start çalıştı");
    
        if (SaveManager.instance == null)
        {
            Debug.LogError("SaveManager bulunamadı!");
            return;
        }
    
        if (levelButtonPrefab == null)
        {
            Debug.LogError("LevelButtonPrefab bağlanmamış!");
            return;
        }
    
        if (contentPanel == null)
        {
            Debug.LogError("ContentPanel bağlanmamış!");
            return;
        }
    
        Debug.Log("Herşey tamam, butonlar oluşturuluyor...");
        saveManager = SaveManager.instance;
        GenerateLevelButtons();
    }

    public void GenerateLevelButtons()
    {
        for (int i = 1; i <= totalLevels; i++)
        {
            GameObject buttonObj = Instantiate(levelButtonPrefab, contentPanel);
            Button levelButton = buttonObj.GetComponent<Button>();
            TextMeshProUGUI levelNumberText = levelButton.GetComponentInChildren<TextMeshProUGUI>();
            levelNumberText.text = i.ToString();

            int stars = saveManager.GetLevelStars(i);
            SetStars(buttonObj.transform, stars);

            bool unlocked = saveManager.IsLevelUnlocked(i);
            levelButton.interactable = unlocked;

            int levelIndex = i;
            levelButton.onClick.AddListener(() => LoadLevel(levelIndex));
        }
    }

    public void SetStars(Transform buttonTransform, int starCount)
    {
        Transform star1Transform = buttonTransform.Find("StarsContainer/Star1");
        Transform star2Transform = buttonTransform.Find("StarsContainer/Star2");
        Transform star3Transform = buttonTransform.Find("StarsContainer/Star3");

        if (star1Transform == null || star2Transform == null || star3Transform == null)
        {
            Debug.LogError("Yıldızlar bulunamadı! Prefab'ta Star1, Star2, Star3 objelerini kontrol et.");
            return;
        }

        Image star1 = star1Transform.GetComponent<Image>();
        Image star2 = star2Transform.GetComponent<Image>();
        Image star3 = star3Transform.GetComponent<Image>();

        Color activeColor = new Color(1f, 0.84f, 0f);
        Color inactiveColor = new Color(0.39f, 0.39f, 0.39f);

        if (starCount >= 1) { star1.color = activeColor; } else { star1.color = inactiveColor; }
        if (starCount >= 2) { star2.color = activeColor; } else { star2.color = inactiveColor; }
        if (starCount >= 3) { star3.color = activeColor; } else { star3.color = inactiveColor; }
    }

    public void LoadLevel(int levelNumber)
    {
        Debug.Log("Loading Level " + levelNumber);
        int sceneIndex = levelNumber + 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

    public void BackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}