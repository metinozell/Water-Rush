using UnityEngine;

public class WaterContainer : MonoBehaviour
{
    public static WaterContainer instance;
    public float currentWater = 100f;
    public float maxWater = 100f;
    public float waterLossRate = 0f;
    public GameHUD gameHUD;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        gameHUD = FindObjectOfType<GameHUD>();
    }
    public void LoseWater(float amount)
    {
        currentWater -= amount;

    }
    public float GetWaterPercentage()
    {
        return currentWater / maxWater;
    }

    void Update()
    {
        currentWater -= waterLossRate * Time.deltaTime;
        currentWater = Mathf.Clamp(currentWater, 0, maxWater);
        Debug.Log("Current Water: " + currentWater);
        Debug.Log("Water Percentage: " + GetWaterPercentage());
        gameHUD.UpdateWaterUI(GetWaterPercentage());
    }
}
