using UnityEngine;

public class WindZone : MonoBehaviour
{
    public float windForce = 0.5f;
    private Vector3 windDirection = Vector3.right;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WaterContainer.instance.LoseWater(windForce * Time.deltaTime);
            Debug.Log("Player is in the wind zone and losing water due to wind force.");
        }
    }

}
