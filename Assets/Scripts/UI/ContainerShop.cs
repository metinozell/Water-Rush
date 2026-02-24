using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContainerShop : MonoBehaviour
{
    public static ContainerShop instance;
    public GameObject containerCardPrefab;
    public Transform scrollContent;
    public TextMeshProUGUI totalStarsText;
    private bool canAfford;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
 Debug.Log("ContainerShop Start çalıştı");
    
    if (ContainerManager.instance == null)
    {
        Debug.LogError("ContainerManager.instance NULL!");
        return;
    }
    
    if (containerCardPrefab == null)
    {
        Debug.LogError("containerCardPrefab bağlanmamış!");
        return;
    }
    
    if (scrollContent == null)
    {
        Debug.LogError("scrollContent bağlanmamış!");
        return;
    }
    
    Debug.Log("Herşey tamam, kartlar oluşturuluyor...");
    UpdateTotalStars();
    GenerateContainerCards();
    }

    public void UpdateTotalStars()
    {
        int totalStars = SaveManager.instance.GetTotalStars();
        totalStarsText.text = "Stars: " + totalStars.ToString();
    }

    public void SetupCard(GameObject card, ContainerData containerData, int index)
    {
        Image iconImage = card.transform.Find("Icon").GetComponent<Image>();
        TextMeshProUGUI nameText = card.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI descText = card.transform.Find("Desc").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI statsText = card.transform.Find("Stats").GetComponentInParent<TextMeshProUGUI>();
        Button selectButton = card.transform.Find("SelectButton").GetComponent<Button>();
        Button unlockButton = card.transform.Find("UnlockButton").GetComponent<Button>();
        Image lockIcon = card.transform.Find("LockIcon").GetComponent<Image>();

        iconImage.sprite = containerData.containerIcon;
        nameText.text = containerData.name;
        descText.text = containerData.description;
        statsText.text = "Capacity: " + containerData.waterCapacity + "L\nStability: " + CalculateStability(containerData.waterLossMultiplier);
        bool isUnlocked = ContainerManager.instance.IsContainerUnlocked(index);
        bool isSelected = ContainerManager.instance.GetCurrentContainer() == containerData;

        if (isUnlocked)
        {
            lockIcon.gameObject.SetActive(false);
            unlockButton.gameObject.SetActive(false);

            if (isSelected)
            {
                selectButton.GetComponentInChildren<TextMeshProUGUI>().text = "Selected";
                selectButton.interactable = false;
            }
            else
            {
                selectButton.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
                selectButton.interactable = true;
                selectButton.onClick.AddListener(() => OnSelectContainer(index));
            }
        }
        else
        {
            lockIcon.gameObject.SetActive(true);
            unlockButton.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(false);

            unlockButton.GetComponentInChildren<TextMeshProUGUI>().text = "Unlock (" + containerData.unlockCost + " Stars)";
            canAfford = ContainerManager.instance.CanAffordContainer(containerData.unlockCost);
            unlockButton.interactable = canAfford;
            unlockButton.onClick.AddListener(() => OnUnlockContainer(index));
        }

    }

    public string CalculateStability(float waterLossMultiplier)
    {
        if (waterLossMultiplier <= 0.5) return "★★★★★";
        else if (waterLossMultiplier <= 1f) return "★★★☆☆";
        else if (waterLossMultiplier > 1f) return "★☆☆☆☆";
        else return "N/A";
    }

    public void OnSelectContainer(int index)
    {
        ContainerManager.instance.SelectContainer(index);
        GenerateContainerCards();
    }

    public void OnUnlockContainer(int index)
    {
        int totalStars = SaveManager.instance.GetTotalStars();
        int cost = ContainerManager.instance.allContainers[index].unlockCost;

        if (totalStars >= cost)
        {
            ContainerManager.instance.UnlockContainer(index);
            ContainerManager.instance.SelectContainer(index);

            GenerateContainerCards();
            UpdateTotalStars();
            Debug.Log("Container unlocked!");
        }
        else
        {
            Debug.Log("Not enough stars to unlock this container.");
        }
    }

    public void GenerateContainerCards()
    {

        Debug.Log("GenerateContainerCards çağrıldı");
    
    ContainerData[] containers = ContainerManager.instance.allContainers;
    
    if (containers == null || containers.Length == 0)
    {
        Debug.LogError("allContainers boş veya null!");
        return;
    }
    
    Debug.Log("Toplam container sayısı: " + containers.Length);
    
    for (int i = 0; i < containers.Length; i++)
    {
        Debug.Log("Container " + i + " oluşturuluyor...");
        GameObject card = Instantiate(containerCardPrefab, scrollContent);
        Debug.Log("Card oluşturuldu: " + card.name);
        
        // SetupCard(card, containers[i], i);
    }

        /*
        ContainerData[] containers = ContainerManager.instance.allContainers;
        foreach (ContainerData container in containers)
        {
            GameObject card = Instantiate(containerCardPrefab, scrollContent);
            SetupCard(card, container, 1);
        }*/
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {
        transform.Rotate(Vector3.up, 20f * Time.deltaTime);
    }
}
