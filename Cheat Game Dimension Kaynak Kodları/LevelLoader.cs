using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public static LevelLoader instance;

    public float transitionTime = 1f;

    private Scene thisScene;

    private int lastSceneIndex;

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

        lastSceneIndex = GameObject.FindGameObjectWithTag("LevelChecker").GetComponent<LevelChecker>().currentLevelIndex;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void RestartLevel()
    {
        StartCoroutine(LoadLevel(lastSceneIndex));
    }

    public void NextLevel()
    {
          StartCoroutine(LoadLevel(lastSceneIndex + 1));
    }

    public void LoadGame()
    {
       StartCoroutine(LoadLevel(8));
    }

    public void LoadSpesificLevel(int levelIndex)
    {
        StartCoroutine(LoadLevel(levelIndex));
    }

    public void LoadMainMenu()
    {
       StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int sceneIndex)
    {
        //animasyon
        transition.SetTrigger("Start");

        //bekle
        yield return new WaitForSeconds(transitionTime);

        //sahneyi yükle
        SceneManager.LoadScene(sceneIndex);

        yield return new WaitForSeconds(1);

        lastSceneIndex = GameObject.FindGameObjectWithTag("LevelChecker").GetComponent<LevelChecker>().currentLevelIndex;
    }
}
