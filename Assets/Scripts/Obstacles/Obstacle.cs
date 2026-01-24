using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Obstacle : MonoBehaviour
{
    public float waterDamage = 15f;


    void Start()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WaterContainer.instance.LoseWater(waterDamage);
            Debug.Log("Player hit an obstacle and lost " + waterDamage + " water.");
        }
    }
}