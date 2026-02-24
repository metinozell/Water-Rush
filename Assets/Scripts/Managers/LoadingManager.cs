using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public Slider loadingSlider;
    public float loadingDuration = 3f; 

    void Start()
    {
        if (loadingSlider != null)
        {
            loadingSlider.value = 0;
            StartCoroutine(StartLoading());
        }
        else
        {
            Debug.LogError("Lütfen Inspector üzerinden Loading Slider'ı bağla!");
        }
    }

    IEnumerator StartLoading()
    {
        float elapsedTime = 0f;

        while (elapsedTime < loadingDuration)
        {
            elapsedTime += Time.deltaTime;
            loadingSlider.value = elapsedTime / loadingDuration; 
            yield return null; 
        }

        loadingSlider.value = 1f; 
        SceneManager.LoadScene("MainMenu"); 
    }
}