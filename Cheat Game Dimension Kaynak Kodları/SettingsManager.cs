using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer mainMixer;

    public bool[] levels = new bool[10];

    public float
        sfxVolume = 0.5f,
        musicVolume = 0.5f;

    public Slider
        sfxSlider,
        musicSlider;

    public static SettingsManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            for (int i = 0; i < levels.Length; i++)
            {
                levels[i] = true;
            }

            CheckLevelLock();
        }
    }

    public void CheckLevelLock()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            GameObject.FindGameObjectWithTag("LevelSelect").GetComponent<LevelSelect>().SetActiveOrNot(i,levels[i]);

            Debug.Log("Level "+ i + " = " + levels[i]);
        }
    }

    public void SetVolume()
    {
        sfxVolume = sfxSlider.value;
        musicVolume = musicSlider.value;

        Debug.Log("SFX: " + sfxVolume);
        Debug.Log("Müzik: " + musicVolume);
    }


    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("volume", volume);
    }
}
