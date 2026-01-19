using System.Collections;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    public static PlantGrowth instance;
    public Vector3 minScale = new Vector3(0.3f, 0.5f, 0.3f);
    public Vector3 maxScale = new Vector3(1f, 3f, 1f);
    public float growthDuration = 2f;
void Awake()
{
    if (instance == null)
        instance = this;
    else
        Destroy(gameObject);
}
    void Start()
    {
        transform.localScale = minScale;
    }
    public void GrowPlant(float waterPercentage)
    {
        Vector3 targetScale = Vector3.Lerp(minScale, maxScale, waterPercentage);
       StartCoroutine(GrowthAnimation(waterPercentage));
    }

    IEnumerator GrowthAnimation(float waterPercentage)
    {
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = Vector3.Lerp(minScale, maxScale, waterPercentage);
        
        float elapsed = 0f;
        
        while (elapsed < growthDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / growthDuration;
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }
        
        transform.localScale = targetScale;
    }
}
