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



    internal ScreenWarp screenWarp;







    void OnEnable()

    {

        if (actions == null)

        {

            actions = new InputSystem_Actions();

            actions.Player.Move.performed += i => moveInput = i.ReadValue<Vector2>();

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

        if (moveTimer >= moveSpeed)

        {

            if (moveInput.magnitude > 0.5f)

                currentDirection = moveInput;

            Move(currentDirection, tail);

            moveTimer = 0f;

            FacedDireciton();

            var objects = Physics2D.OverlapCircleAll(head.transform.position, 0.5f);

            foreach (var obj in objects)

            {

                OnCollisionEnter2D(obj);

            }

        }



    }

    private void FacedDireciton()

    {

        if (Mathf.Abs(currentDirection.x) > Mathf.Abs(currentDirection.y)) // mostly horizontal

        {

            if (currentDirection.x > 0) // moving right

            {

                head.transform.rotation = Quaternion.Euler(0, 0, 0);

                head.GetComponent<SpriteRenderer>().flipX = true;

                pacsnakeRenderer.sprite = pacHeadRight[0];

            }

            else // moving left

            {

                head.transform.rotation = Quaternion.Euler(0, 0, 0);

                head.GetComponent<SpriteRenderer>().flipX = false;

                pacsnakeRenderer.sprite = pacHeadLeft[0];

            }

        }

        else

        {

            if (currentDirection.y > 0) // moving up

            {

                head.transform.rotation = Quaternion.Euler(0, 0, 90);

                head.GetComponent<SpriteRenderer>().flipX = true;

                pacsnakeRenderer.sprite = pacHeadUp[0];

            }

            else // moving down

            {

                head.transform.rotation = Quaternion.Euler(0, 0, 90);

                head.GetComponent<SpriteRenderer>().flipX = false;

                pacsnakeRenderer.sprite = pacHeadDown[0];

            }

        }

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



    public void OnCollisionEnter2D(Collider2D collision)

    {

        if (collision == null) return;



        if (collision.TryGetComponent<IWallInteractable>(out var wall))

        {

            wall.OnWallCollision();

            return;

        }



        if (collision.TryGetComponent<IGhostInteractable>(out var ghost))

        {

            ghost.OnGhostCollision();

            Grow();

            return;

        }



        if (collision.TryGetComponent<IPelletInteractable>(out var pellet))

        {

            pellet.OnPelletCollision();

            return;

        }



        if (collision.TryGetComponent<IFruitInteractable>(out var fruit))

        {

            fruit.OnFruitCollision();

            return;

        }



        if (collision.CompareTag("Player"))

        {

            //Die();

        }

    }

    public Vector2 GetInput()

    {





        return moveInput;

    }

    public void Die()

    {

        Destroy(gameObject);

        // Implement logic for PacSnake death

    }

    public void Respawn()

    {

        // Implement logic for PacSnake respawn

    }
}

