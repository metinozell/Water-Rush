using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ShopUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI containerNameText;
    public TextMeshProUGUI costText;
    public Button actionButton; 
    public TextMeshProUGUI actionButtonText;
    
    public Image containerDisplayImage;
    
    private int currentIndex = 0;

    void Start()
    {
        UpdateUI();
    }

    public void NextItem()
    {
        currentIndex = (currentIndex + 1) % ContainerManager.instance.allContainers.Length;
        UpdateUI();
    }

    public void PreviousItem()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = ContainerManager.instance.allContainers.Length - 1;
        UpdateUI();
    }

    void UpdateUI()
    {
        ContainerData data = ContainerManager.instance.allContainers[currentIndex];
        containerNameText.text = data.containerName;
        containerDisplayImage.sprite = data.containerSprite; 
        coinText.text = SaveManager.instance.GetTotalCoins().ToString();

        if (ContainerManager.instance.IsContainerUnlocked(currentIndex))
        {
            costText.gameObject.SetActive(false);
            actionButtonText.text = "SELECT";
        }
        else
        {
            costText.gameObject.SetActive(true);
            costText.text = data.unlockCost.ToString() + " COINS";
            actionButtonText.text = "BUY";
        }
    }

    public void OnActionClick()
    {
        if (ContainerManager.instance.IsContainerUnlocked(currentIndex))
        {
            ContainerManager.instance.SelectContainer(currentIndex);
        }
        else
        {
            int cost = ContainerManager.instance.allContainers[currentIndex].unlockCost;
            if (SaveManager.instance.SpendCoins(cost))
            {
                ContainerManager.instance.UnlockContainer(currentIndex);
                ContainerManager.instance.SelectContainer(currentIndex);
            }
            else
            {
                Debug.Log("Not enough coins to unlock this container!");
            }
        }
        UpdateUI();
    }
}