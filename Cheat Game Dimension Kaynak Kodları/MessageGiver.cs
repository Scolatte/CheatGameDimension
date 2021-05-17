using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageGiver : MonoBehaviour
{
    public LayerMask playerLayer;

    public string message;

    private BoxCollider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (coll.IsTouchingLayers(playerLayer))
        {
            GameObject.FindGameObjectWithTag("LogPanel").GetComponent<LogPanel>().ShowMessage(message);
            Destroy(gameObject);
        }
    }
}
