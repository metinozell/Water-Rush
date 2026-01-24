using UnityEngine;

public class Ramp : MonoBehaviour
{
    public float rampDamage = 5f;

    void OnCollisionEnter(Collision collision)
    {
        WaterContainer waterContainer = collision.gameObject.GetComponent<WaterContainer>();
        waterContainer.LoseWater(rampDamage);
        Debug.Log("Player collided with ramp and lost " + rampDamage + " water.");
    }
}
