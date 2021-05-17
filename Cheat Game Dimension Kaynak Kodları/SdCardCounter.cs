using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SdCardCounter : MonoBehaviour
{
    public GameObject portal;

    public GameObject[] sdCards = new GameObject[3];

    public int sdCardCount = 0;

    private GameObject SdCarUI;

    // Start is called before the first frame update
    void Start()
    {
        SdCarUI = GameObject.FindGameObjectWithTag("SdCard");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSdCard()
    {
        sdCardCount += 1;
        CalculateSdCardCount();
    }

    private void CalculateSdCardCount()
    {
        for (int i = 0; i < sdCardCount; i++)
        {
            sdCards[i].GetComponent<Image>().color = new Color (255,255,225,255);
        }

        if (sdCardCount == 1)
        {
            GameObject.FindObjectOfType<AudioManager>().Play("Card1");
        }
        else if(sdCardCount == 2)
        {
            GameObject.FindObjectOfType<AudioManager>().Play("Card2");
        }
        else if (sdCardCount == 3)
        {
            GameObject.FindObjectOfType<AudioManager>().Play("Card3");
            StartCoroutine(CardMelody());
        }

        if (sdCardCount == 3)
        {
            Instantiate(portal, GameObject.FindGameObjectWithTag("EndPortalSpawnPoint").transform.position, Quaternion.identity);
        }
    }

    IEnumerator CardMelody()
    {
        yield return new WaitForSeconds(1);
        GameObject.FindObjectOfType<AudioManager>().Play("CardMelody");
    }
}
