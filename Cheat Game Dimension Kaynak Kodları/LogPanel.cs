using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogPanel : MonoBehaviour
{
    public GameObject text, image;

    private float lastMessageTime;

    public float messageTime;
    // Start is called before the first frame update
    private void Awake()
    {
        lastMessageTime = -messageTime - 1;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >lastMessageTime + messageTime)
        {
            image.SetActive(false);
            text.SetActive(false);
        }
    }

    public void ShowMessage(string message)
    {
        text.SetActive(true);
        text.GetComponent<TextMeshProUGUI>().text = message;
        image.SetActive(true);
        lastMessageTime = Time.time;
    }
}
