using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1); // loads next scene
    }

    public void QuitGame()
    {
        Debug.Log("You have quit the game :(");
        Application.Quit(); // quits the game
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // freeze game
        GameIsPaused = true;
    }
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // freeze game
        GameIsPaused = false;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }

            }
        }
    }

}
