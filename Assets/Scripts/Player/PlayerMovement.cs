using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public float moveSpeed = 5f;
    public bool isMoving;
    private Rigidbody rb;
    public GameObject endPoint;
    public GameObject startPoint;
    public int currentLane = 1; // 0 = left, 1 = center, 2 = right
    public float laneDistance = 2f;
    public float laneSpeed = 10f; // Speed of lane switching
    float targetX;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
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
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.D))
            ChangeLane(1);
        else if (Input.GetKeyDown(KeyCode.A))
            ChangeLane(-1);

        float newX = Mathf.Lerp(
            transform.position.x,
            targetX,
            laneSpeed * Time.deltaTime
        );

        transform.position = new Vector3(
            newX,
            transform.position.y,
            transform.position.z
        );
    }
}
