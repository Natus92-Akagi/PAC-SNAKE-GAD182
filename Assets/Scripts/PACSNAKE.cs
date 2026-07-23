using UnityEngine;
using System.Collections.Generic;

public class PACSNAKE : MonoBehaviour, IPacSnakeable, ICollisionable, IInputable, IDeathable
{
    [SerializeField] SpriteRenderer pacsnakeRenderer;
    [SerializeField] Sprite[] pacHeadUp;
    [SerializeField] Sprite[] pacHeadDown;
    [SerializeField] Sprite[] pacHeadLeft;
    [SerializeField] Sprite[] pacHeadRight;
    [SerializeField] GameObject bodySegmentPrefab;
    [SerializeField] GameObject tail;
    [SerializeField] GameObject head;

    private Vector2 currentDirection = Vector2.right;
    private List<Transform> bodySegments = new List<Transform>();
    private int pendingGrowth = 0;

    private float moveTimer;
    public float moveSpeed = 0.2f;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer += Time.deltaTime;
        if ( moveTimer >= moveSpeed)
        {
            Move(currentDirection, tail);
            moveTimer = 0f;
        }
    }

    public void Move(Vector2 direction, GameObject tail)
    {
        currentDirection = direction;

        Vector2 oldPosition = head.transform.position;

        head.transform.position += (Vector3)currentDirection;

        GameObject newSegment = Instantiate(bodySegmentPrefab, oldPosition, Quaternion.identity);
        bodySegments.Insert(0, newSegment.transform);


        if (pendingGrowth > 0)
        {

            pendingGrowth--;
        }
        else
        {
           
            Transform tailTransform = bodySegments[bodySegments.Count - 1];
            bodySegments.RemoveAt(bodySegments.Count - 1);
            Destroy(tailTransform.gameObject);
        }
    }
   
    public void Grow()
    {
        pendingGrowth += 1;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        var interactable = collision.gameObject.GetComponent<IWallInteractable>();
        if (interactable != null)
        {
            interactable.OnWallCollision();
            return;
        }
        var interactable2 = collision.gameObject.GetComponent<IGhostInteractable>();
        if (interactable2 != null)
        {
            interactable2.OnGhostCollision();
            return;
        }
        var interactable3 = collision.gameObject.GetComponent<IPelletInteractable>();
        if (interactable3 != null)
        {
            interactable3.OnPelletCollision();
            return;
        }
        var interactable4 = collision.gameObject.GetComponent<IFruitInteractable>();
        if (interactable4 != null)
        {
            interactable4.OnFruitCollision();
            return;
        }
    }
    public Vector2 GetInput()
    {
        // Implement logic to get input from the player
        return Vector2.zero;
    }
    public void Die()
    {
        // Implement logic for PacSnake death
    }
    public void Respawn()
    {
        // Implement logic for PacSnake respawn
    }





}
