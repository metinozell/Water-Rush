using UnityEngine;
using System.Collections;

public class GameStarter : MonoBehaviour
{
    public static GameStarter instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    IEnumerator startGameCoroutine()
    {
        yield return new WaitForSeconds(2f);
        
    }
    void Start()
    {
        StartCoroutine(startGameCoroutine());
        //Partical Effect
        GameManager.instance.StartGame();
        WaterContainer waterContainer = FindObjectOfType<WaterContainer>();
        waterContainer.currentWater = 100f;
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        player.isMoving = true;
    }
    
}
