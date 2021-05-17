using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SdCard : MonoBehaviour
{
    public GameObject blast;

    public LayerMask playerLayer;

    private Animator anim;
    private BoxCollider2D coll;

    private bool isItDone;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coll.IsTouchingLayers(playerLayer) && !isItDone)
        {
            Instantiate(blast, transform.position, Quaternion.identity);

            GameObject.FindGameObjectWithTag("SdCardCounter").GetComponent<SdCardCounter>().AddSdCard();
            isItDone = true;

            Suicide();
        }
        
    }

    public void Suicide()
    {
        Destroy(this.gameObject);
    }
}
