using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameHUD : MonoBehaviour
{
    public static GameHUD instance;
    public Slider waterSlider;
    public Button nextLevelButton;
    public GameObject endPanel;
    public Transform starsContainer;
    public Color grayColor = new Color(0.39f, 0.39f, 0.39f);
    public Color goldColor = new Color(1f, 0.84f, 0f);
    public float animationDelay = 0.5f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowStars(int starCount)
    {
        Debug.Log("ShowStars çağrıldı! Star count: " + starCount);
        
        if (starsContainer == null)
        {
            Debug.LogError("StarsContainer bağlanmamış!");
            return;
        }

        Image[] stars = starsContainer.GetComponentsInChildren<Image>();
        
        if (stars == null || stars.Length != 3)
        {
            Debug.LogError("StarsContainer içinde 3 Image bulunamadı! Bulunan: " + (stars != null ? stars.Length : 0));
            return;
        }
        
        Debug.Log("3 yıldız bulundu, animasyon başlıyor!");
        StartCoroutine(ShowStarsAnimated(stars, starCount));
    }

    IEnumerator ShowStarsAnimated(Image[] stars, int starCount)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].color = grayColor;

            if (i < starCount)
            {
                stars[i].transform.localScale = Vector3.zero;
            }
            else
            {
                stars[i].transform.localScale = Vector3.one;
            }
        }

        for (int i = 0; i < starCount && i < stars.Length; i++)
        {
            yield return new WaitForSeconds(animationDelay);
            stars[i].color = goldColor;
            yield return StartCoroutine(ScaleStar(stars[i].transform));
            StartCoroutine(SparkleEffect(stars[i]));
        }
    }

    IEnumerator SparkleEffect(Image star)
    {
        Color originalColor = star.color;
    
        for (int i = 0; i < 3; i++)
        {
            star.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            star.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator ScaleStar(Transform starTransform)
    {
        float time = 0f;
    float duration = 0.5f;
    float rotationSpeed = 720f;

    Vector3 startScale = Vector3.zero;
    Vector3 targetScale = Vector3.one * 1.2f;

    while (time < duration)
    {
        time += Time.deltaTime;
        float t = time / duration;
        
        float bounceT = Mathf.Sin(t * Mathf.PI);
        starTransform.localScale = Vector3.Lerp(startScale, targetScale, bounceT);
        
        starTransform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        
        yield return null;
    }

    float bounceBackTime = 0f;
    float bounceBackDuration = 0.2f;
    
    while (bounceBackTime < bounceBackDuration)
    {
        bounceBackTime += Time.deltaTime;
        float t = bounceBackTime / bounceBackDuration;
        starTransform.localScale = Vector3.Lerp(targetScale, Vector3.one, t);
        yield return null;
    }

    starTransform.localScale = Vector3.one;
    starTransform.rotation = Quaternion.identity;
    }

    public void ShowEndPanel()
    {
        if (endPanel != null)
        {
            endPanel.SetActive(true);
        }
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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        
        if (nextSceneIndex < totalScenes && nextSceneIndex >= 2)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}