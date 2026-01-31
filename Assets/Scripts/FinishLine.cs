using System.Collections;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private bool finished = false;
    public static FinishLine instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (finished) return;

        if (other.CompareTag("Player"))
        {
            finished = true;
            Debug.Log("Finish Line Reached!");
            StartCoroutine(EndGameRoutine());
        }
    }

    IEnumerator EndGameRoutine()
    {

        if (PlayerMovement.instance != null)
        {
            PlayerMovement.instance.enabled = false;
            PlayerMovement.instance.isMoving = false;
        }

        float water = WaterContainer.instance.GetWaterPercentage();
        Debug.Log("SU YÜZDES: " + water + " (" + (water * 100) + "%)");
    
        int stars = ScoreManager.instance.CalculateStars(water);
        Debug.Log("HESAPLANAN YILDIZ: " + stars);
        if (PlayerMovement.instance != null)
        {
            PlayerMovement.instance.enabled = false;
            PlayerMovement.instance.isMoving = false;
        }

        //float water = WaterContainer.instance.GetWaterPercentage();
        //int stars = ScoreManager.instance.CalculateStars(water);
        
        SaveManager.instance.SaveLevelProgress(GameManager.instance.currentLevel, stars);
        SaveManager.instance.UnlockLevel(GameManager.instance.currentLevel + 1);
        
        PlantGrowth.instance.GrowPlant(water);
        
        yield return new WaitForSeconds(3f);
        
        GameHUD.instance.ShowStars(stars);
        
        yield return new WaitForSeconds(2f);
        
        GameHUD.instance.ShowEndPanel();
        
        Debug.Log("Game Ended!");
    }
}