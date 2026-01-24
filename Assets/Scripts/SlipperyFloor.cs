using UnityEngine;

public class SlipperyFloor : MonoBehaviour
{
    public float slipMultiplier = 1.5f;

    void OnTriggerStay(Collider other)
    {
        WaterContainer waterContainer = other.GetComponent<WaterContainer>();
        WaterContainer.instance.waterLossRate *= slipMultiplier;
        Debug.Log("Player is on slippery floor, increasing water loss rate.");
    }

    void OnTriggerExit(Collider other)
    {
        WaterContainer.instance.waterLossRate /= slipMultiplier;
        Debug.Log("Player exited slippery floor, restoring water loss rate.");
    }
}
