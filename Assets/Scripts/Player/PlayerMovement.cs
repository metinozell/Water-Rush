using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public float moveSpeed = 5f;
    
    public bool isMoving; 

    private Rigidbody rb;
    public int currentLane = 1; 
    public float laneDistance = 2f;
    public float laneSpeed = 10f;
    float targetX;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        targetX = transform.position.x;
        rb = GetComponent<Rigidbody>();
    }

    void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, 0, 2);
        targetX = (currentLane - 1) * laneDistance;
    }

    void Update()
    {
        if (GameManager.instance.currentState != GameManager.GameState.Playing)
            return;

        if (Input.GetKeyDown(KeyCode.D))
            ChangeLane(1);
        else if (Input.GetKeyDown(KeyCode.A))
            ChangeLane(-1);
    }

    void FixedUpdate()
    {
        if (GameManager.instance.currentState != GameManager.GameState.Playing)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }
        float xDifference = targetX - rb.position.x;
        rb.linearVelocity=new Vector3(xDifference * laneSpeed, rb.linearVelocity.y, moveSpeed);
    }
}