using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject controlLayout;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controlLayout.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Easy Mode");
    }

    public void ShowControls()
    {
        controlLayout.SetActive(true);
    }
    public void HideControls()
    {
        controlLayout.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

}
