using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    public static ContainerManager instance;
    public ContainerData[] allContainers;
    private ContainerData currentContainer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSelectedContainer();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSelectedContainer()
    {
        int currentContainerIndex = PlayerPrefs.GetInt("SelectedContainerIndex", 0);
        if (currentContainerIndex >= 0 && currentContainerIndex < allContainers.Length)
        {
            currentContainer = allContainers[currentContainerIndex];
        }
        else
        {
            currentContainer = allContainers[0];
        }
    }

    public void SelectContainer(int index)
    {
        currentContainer = allContainers[index];
        PlayerPrefs.SetInt("SelectedContainerIndex", index);
        PlayerPrefs.Save();
    }

    public ContainerData GetCurrentContainer()
    {
        if(currentContainer == null) LoadSelectedContainer();
        return currentContainer;
    }

    public bool IsContainerUnlocked(int index)
    {
        if (index == 0) return true;
        return PlayerPrefs.GetInt("Container_" + index + "_Unlocked", 0) == 1;
    }

    public void UnlockContainer(int index)
    {
        PlayerPrefs.SetInt("Container_" + index + "_Unlocked", 1);
        PlayerPrefs.Save();
    }

    public bool CanAffordContainer(int index)
    {
        int cost = allContainers[index].unlockCost;
        int totalCoins = SaveManager.instance.GetTotalCoins();
        return totalCoins >= cost;
    }
}