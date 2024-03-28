using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [Header("All Menu")]
    public GameObject pauseMenuUi;
    public GameObject EndgameMenuUi;
    public GameObject ObjectiveMenuUi;

    public static bool GameisStopped = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameisStopped)
            {
                Resume();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Pause();
                Cursor.lockState = CursorLockMode.None;
            }    
        }
        else if (Input.GetKeyDown("m"))
        {
            if(GameisStopped)
            {
                removeObjectives();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                showObjectives();
                Cursor.lockState= CursorLockMode.None;
            }
        }
    }
    public void showObjectives()
    {
        ObjectiveMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameisStopped = true;
    }
    public void removeObjectives()
    {
        ObjectiveMenuUi.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        GameisStopped = false;
    }
    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameisStopped= false;

    }
    public void Restart()
    {
        SceneManager.LoadScene("ZombieLand");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game .....");
        Application.Quit();
    }
    void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameisStopped = true;
    }
}
