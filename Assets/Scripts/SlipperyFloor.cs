using UnityEngine;

public class SlipperyFloor : MonoBehaviour
{
    public float slipMultiplier = 1.5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WaterContainer.instance.waterLossRate *= slipMultiplier;
            Debug.Log("The character ENTERED the slippery floor, the water leak " + slipMultiplier + " increased by a floor.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WaterContainer.instance.waterLossRate /= slipMultiplier;
            Debug.Log("The character EXITED the slippery floor, the water leak rate returned to normal.");
        }
    }
}
