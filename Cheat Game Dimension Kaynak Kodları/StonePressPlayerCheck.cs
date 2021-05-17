using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePressPlayerCheck : MonoBehaviour
{
    public GameObject stonePress;

    private BoxCollider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Player görüldü");
            stonePress.GetComponent<StonePress>().FastPressDown();
        }
    }
}
