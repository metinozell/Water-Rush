using UnityEngine;

[CreateAssetMenu(fileName = "ContainerData", menuName = "ScriptableObjects/ContainerData", order = 1)]
public class ContainerData : ScriptableObject
{
    public string containerName;
    public string description;
    public Sprite containerIcon;
    public GameObject containerPrefab;
    public float waterCapacity;
    public float waterLossMultiplier;
    public int unlockCost;
    public bool isUnlocked;
    public Sprite containerSprite; 
}
