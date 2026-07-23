using UnityEngine;

public class Ghosts : MonoBehaviour, IGhostInteractable, IDeathable, IScoreable
{
    public enum GhostType
    {
        Binky,
        Clyde,
        Inky,
        Pinky
    }
    public GhostType ghostName;

    public CircleCollider2D ghostDetectionCollider;

    private static int totalGhostEaten = 0;
    private static int numberOfGhosts = 4;

    private int ghostScoreValue = 200;
    public GameObject[] ghostPrefabs;

    private float ghostSpeed = 0.3f;
    private float ghostFleeSpeed = 0.8f;

    private Vector2 roamDirection;
    private float roamDuration;

    public static int globalScore = 0;

    private float fleeTimer = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeRoamDirection();
    }

    // Update is called once per frame
    void Update()
    {
        roamDuration -= Time.deltaTime;
        if (roamDuration <= 0)
        {
            ChangeRoamDirection();
            roamDuration = Random.Range(2f, 5f); // Random duration for roaming
        }
        transform.Translate(roamDirection * ghostSpeed * Time.deltaTime);

        float minX = -8.5f;
        float maxX = 8.5f;
        float minY = -4.5f;
        float maxY = 4.5f;

        Vector3 pos = transform.position;
       
        if (pos.x < minX) pos.x = maxX;
        else if (pos.x > maxX) pos.x = minX;

        if (pos.y < minY) pos.y = maxY;
        else if (pos.y > maxY) pos.y = minY;

        transform.position = pos;

        fleeTimer = 2f; // Reset the flee timer to 2 seconds
        if (fleeTimer > 0)
        {
            fleeTimer -= Time.deltaTime;
            Flee();
            return;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnGhostCollision();
        }
    }
    public void OnGhostCollision() 
    { 
        AddScore(ghostScoreValue);
        Die();
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("Player"))
        {
            Flee();
        }
    }
    public void Flee() 
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        {
            Vector2 escapeDirection = (transform.position - playerTransform.position).normalized;

            transform.position += (Vector3)escapeDirection * ghostFleeSpeed * Time.deltaTime;
        }

    }
  
    public int GetScore()
    {
        return globalScore;
    }
    public void AddScore(int score)
    {
        globalScore += score;
    }
    public void Die()
    {
        totalGhostEaten++;

        if (totalGhostEaten >= numberOfGhosts)
        {
            Respawn();
            totalGhostEaten = 0;
        }
        Destroy(gameObject);
    }
    public void Respawn()
    {
        for (int i = 0; i < numberOfGhosts; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f), 0);
            Instantiate(ghostPrefabs[Random.Range(0, ghostPrefabs.Length)], spawnPos, Quaternion.identity);
        }
    }
    void ChangeRoamDirection()
    {
        roamDirection = Random.insideUnitCircle.normalized;
    }

}
