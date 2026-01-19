using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        Start,
        Playing,
        End
    }

    public GameState currentState;

    public void StartGame()
    {
        currentState = GameState.Playing;
    }
   /*public void EndGame()
    {
        currentState = GameState.End;
        GameHUD.instance.ShowEndPanel();
    }*/
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
