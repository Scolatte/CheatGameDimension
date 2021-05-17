using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public GameObject[] levelButtonLocks = new GameObject[10]; 

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("SettingsManager").GetComponent<SettingsManager>().CheckLevelLock();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveOrNot(int levelIndex, bool isUnlocked)
    {
        levelButtonLocks[levelIndex].SetActive(!isUnlocked);
    }
}
