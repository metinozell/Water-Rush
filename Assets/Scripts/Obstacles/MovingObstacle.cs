using UnityEngine;

public class MovingObstacles : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveRange = 2f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newX = startPosition.x + Mathf.PingPong(Time.time * moveSpeed, moveRange * 2) - moveRange;
        transform.position =new Vector3(newX, transform.position.y, transform.position.z);
    }
}
