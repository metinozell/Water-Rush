using System.Collections;
using Unity.VisualScripting;
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
        GameHUD.instance.ShowEndPanel();
        float water = WaterContainer.instance.GetWaterPercentage();
        PlantGrowth.instance.GrowPlant(water);
        yield return new WaitForSeconds(3f);

        Debug.Log("Game Ended!");
    }
}
