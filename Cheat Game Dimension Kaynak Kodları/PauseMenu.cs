using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused;

    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PlayerController.isCheatPanelOn)
        {
            if (isGamePaused)
            {
                ClosePauseMenu();
            }
            else
            {
                OpenPauseMenu();
            }
        }
    }

    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        Pause();
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        Resume();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void Pause()
    {
       
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void ReturnToMainMenu()
    {
        GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().LoadMainMenu();
        ClosePauseMenu();
    }

    public void RestartLevel()
    {
        GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().RestartLevel();
        ClosePauseMenu();
    }

    public void CloseCheatPanel()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ClosecheatPanel();
    }

    public void UIRedSFX()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("UIRed");
    }

    public void UIOnaySFX()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("UIOnay");
    }

    public void SetVolume(float volume)
    {
        GameObject.FindGameObjectWithTag("SettingsManager").GetComponent<SettingsManager>().SetVolume(volume);
    }
}
