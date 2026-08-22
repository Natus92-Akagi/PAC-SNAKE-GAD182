using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManger : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeText;
    private int toalScore = 0;
    private bool isPaused = false;
    private int lives;





    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {

        lives = 3;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
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
        toalScore = Ghosts.GetScore();

        if (scoreText)
        {

            scoreText.text = "" + toalScore;

        }

    }

    private void OnGUI()
    {

        if (isPaused)
        {

            int width = 300;
            int height = 200;
            int x = (Screen.width - width / 2) - (width / 2);
            int y = (Screen.height - height / 2) - (height / 2);

            GUI.Box(new Rect(x, y, width, height), "Game Paused");

            if (GUI.Button(new Rect(x + 50, y + 50, 200, 40), "Resume Play"))
            {
                isPaused = false;
                Time.timeScale = 1f;
            }
            if (GUI.Button(new Rect(x + 50, y + 100, 200, 40), "Restart Game"))
            {
                isPaused = false;
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (GUI.Button(new Rect(x + 50, y + 150, 200, 40), "Quit Game"))
            {
                isPaused = false;
                Time.timeScale = 1f;
                SceneManager.LoadScene("Title");
            }
        }

    }

}
