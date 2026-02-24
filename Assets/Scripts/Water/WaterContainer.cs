using UnityEngine;
using System.Collections;

public class WaterContainer : MonoBehaviour
{
    public static WaterContainer instance;
    public float currentWater = 100f;
    public float maxWater = 100f;
    public float waterLossRate = 0f;
    public GameHUD gameHUD;
    public ContainerData containerData;
    private float originalWaterLossRate;

    [Header("I-Frames (Dokunulmazlık)")]
    public bool isInvincible = false;
    public float invincibilityTime = 0.5f;

    private bool isGameOver = false; 

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
        containerData = ContainerManager.instance?.GetCurrentContainer();

        if (containerData == null)
        {
            Debug.LogError("ContainerData NULL!");
            return;
        }

        maxWater = containerData.waterCapacity;
        currentWater = maxWater;
        originalWaterLossRate = waterLossRate;
    }

    public void LoseWater(float amount)
    {
        if (containerData == null || isInvincible || isGameOver) 
        {
            return; 
        }

        float loss = amount * containerData.waterLossMultiplier;
        currentWater -= loss;
        currentWater = Mathf.Clamp(currentWater, 0, maxWater);
        StartCoroutine(InvincibilityRoutine());
    }
    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;
        
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false; 
    }

    public float GetWaterPercentage()
    {
        return currentWater / maxWater;
    }

    void Update()
    {
        if (GameManager.instance.currentState != GameManager.GameState.Playing)
            return;
            
        if (isGameOver) return;

        currentWater -= waterLossRate * Time.deltaTime;
        currentWater = Mathf.Clamp(currentWater, 0, maxWater);
        gameHUD.UpdateWaterUI(GetWaterPercentage());

        if (currentWater <= 0 && !isGameOver)
        {
            isGameOver = true;
            GameManager.instance.currentState = GameManager.GameState.End;
            GameHUD.instance.ShowFailPanel();
        }
    }
    
    public void PausePassiveWaterLoss()
    {
        waterLossRate = 0f;
    }
    public void ResumePassiveWaterLoss()
    {
        waterLossRate = originalWaterLossRate; 
    }
}