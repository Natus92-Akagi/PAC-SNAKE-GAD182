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
    private int pendingGrowth = 3;

    private float moveTimer;
    public float moveSpeed = 0.2f;
    InputSystem_Actions actions;
    Vector2 moveInput;

    void OnEnable()
    {
        if(actions==null)
        {
            actions= new InputSystem_Actions();
            actions.Player.Move.performed+= i => moveInput = i.ReadValue<Vector2>();
            actions.Enable();
        }
    }

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
            if(moveInput.magnitude>0.5f)
                currentDirection = moveInput;
            Move(currentDirection, tail);
            moveTimer = 0f;
        }
        //float minX = -8.5f;
        //float maxX = 8.5f;
        //float minY = -4.5f;
        //float maxY = 4.5f;

        //Vector3 pos = transform.position;

        //if (pos.x < minX) pos.x = maxX;
        //else if (pos.x > maxX) pos.x = minX;
        
        //if (pos.y < minY) pos.y = maxY;
        //else if (pos.y > maxY) pos.y = minY;
       
        //transform.position = pos;
    }

    public void Move(Vector2 direction, GameObject tail)
    {
        currentDirection = direction;

        Vector2 oldPosition = head.transform.position;

        head.transform.position += (Vector3)currentDirection;

        GameObject newSegment = Instantiate(bodySegmentPrefab, oldPosition, Quaternion.identity);
        newSegment.transform.localScale = new Vector2(0.5f, 0.5f);
        bodySegments.Insert(0, newSegment.transform);


        if (pendingGrowth > 0)
        {

            pendingGrowth--;
        }
        else
        {
           
            Transform tailTransform = bodySegments[bodySegments.Count - 1];
            tail.transform.position = tailTransform.position;
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
        
        
        return moveInput;
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
