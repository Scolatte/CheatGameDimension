using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatRefresher : MonoBehaviour
{
    public LayerMask playerLayer;

    public BoxCollider2D coll, area;

    public GameObject blast;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (coll.IsTouchingLayers(playerLayer))
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isCheated)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isCheated = false;
                Instantiate(blast, transform.position, Quaternion.identity);
                GameObject.FindGameObjectWithTag("LogPanel").GetComponent<LogPanel>().ShowMessage("Hileler Yenilendi");
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ResetCheats();
                GameObject.FindObjectOfType<AudioManager>().Play("Notification");
                Destroy(gameObject);
            }
            else
            {
                GameObject.FindGameObjectWithTag("LogPanel").GetComponent<LogPanel>().ShowMessage("Zaten Hile Hakkın Var");
            }
        }
        
    }
}
