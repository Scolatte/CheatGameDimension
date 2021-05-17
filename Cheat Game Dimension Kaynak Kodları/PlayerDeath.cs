using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0,3);
    }

    public void Dead()
    {
        GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().RestartLevel(); ///BOZUK
        Destroy(this);
    }
}
