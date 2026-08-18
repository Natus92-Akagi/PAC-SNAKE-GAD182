using UnityEngine;

public class ScreenWarp : MonoBehaviour
{
    [SerializeField] private float leftWarpX = -8.5f;
    [SerializeField] private float rightWarpX = 8.5f;

    [SerializeField] private float topWarpY = 4.5f;
    [SerializeField] private float bottomWarpY = -4.5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 pos = rb.position;

        if (pos.x < leftWarpX)
        {
            pos.x = rightWarpX;
        }
        else if (pos.x > rightWarpX)
        {
            pos.x = leftWarpX;
        }
        
        if (pos.y < bottomWarpY)
        {
            pos.y = topWarpY;
        }
        else if (pos.y > topWarpY)
        {
            pos.y = bottomWarpY;
        }

        rb.position = pos;
    }


}
