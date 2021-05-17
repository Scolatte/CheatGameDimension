using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonPressed()
    {
        GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().LoadGame();
    }

    public void LevelSelectButtonPressed(int levelIndex)
    {
        if (GameObject.FindGameObjectWithTag("SettingsManager").GetComponent<SettingsManager>().levels[levelIndex - 1])
        {
            GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().LoadSpesificLevel(levelIndex);
        }
        else
        {
            GameObject.FindObjectOfType<AudioManager>().Play("UIRed");
        }
    }

    public void ButtonOverSound()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("MouseOver");
    }

    public void MouseClick()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("MouseClick");
    }

    public void SetVolume(float volume)
    {
        GameObject.FindGameObjectWithTag("SettingsManager").GetComponent<SettingsManager>().SetVolume(volume);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
