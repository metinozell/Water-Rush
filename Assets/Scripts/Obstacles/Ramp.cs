using UnityEngine;

public class Ramp : MonoBehaviour
{
    public float rampDamage = 5f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            WaterContainer waterContainer = collision.gameObject.GetComponent<WaterContainer>();
            if (waterContainer != null)
            {
                waterContainer.LoseWater(rampDamage);
                Debug.Log("Player collided with ramp and lost " + rampDamage + " water.");
            }
        }
    }
}