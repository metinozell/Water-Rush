using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public int CalculateStars(float waterPercentage)
    {
        Debug.Log("CalculateStars çağrıldı! Water: " + waterPercentage);
        
        if (waterPercentage >= 0.9f)
        {
            Debug.Log("3 yıldız!");
            return 3;
        }
        if (waterPercentage >= 0.6f)
        {
            Debug.Log("2 yıldız!");
            return 2;
        }
        if (waterPercentage >= 0.3f)
        {
            Debug.Log("1 yıldız!");
            return 1;
        }
        
        Debug.Log("0 yıldız (Failed)");
        return 0;
    }
}
