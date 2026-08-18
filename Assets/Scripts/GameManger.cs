using UnityEngine;
using TMPro;

public class GameManger : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int toalScore = 0;
    private bool isPaused = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetButtonDown("Start"))
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
        }
        UpdateScoreDisplay();
    }
    public void UpdateScoreDisplay()
    {
        toalScore = 0;
        IScoreable[] scoreables = FindObjectsOfType<MonoBehaviour>() as IScoreable[];

        if (scoreText)
        {
            scoreText.text = "Score: " + toalScore;
        }
    }
    private void OnGUI()
    {
        if (isPaused)
        {
            int width = 300;
            int height = 150;
            int x = (Screen.width - width/2)-(width/2);
            int y = (Screen.height - height/2)-(height/2);

            GUI.Box(new Rect(x, y, width, height), "Game Paused");

            if (GUI.Button(new Rect(x+50,y+50,200,40), "Resume Play"))
            {
                isPaused = false;
                Time.timeScale = 1f;
            }
        }
    }
}

