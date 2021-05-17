using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePress : MonoBehaviour
{
    public float speed;

    public Transform maxDown, maxUp;

    public bool isGoingDown, isGoingDownFast;

    private Rigidbody2D rb;
  
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PressCycle();
    }

    private void PressCycle()
    {
        if (isGoingDownFast)
        {
            if (maxDown.position.y < transform.position.y)
            {
                rb.velocity = new Vector2(0, Time.deltaTime * -100 * speed );
            }
            else
            {
                isGoingDownFast = false;
            }
        }
        else
        {
            if (isGoingDown)
            {
                if (maxDown.position.y < transform.position.y)
                {
                    rb.velocity = new Vector2(0, Time.deltaTime * -100 * speed);
                }
                else
                {
                    isGoingDown = false;
                }
            }
            else
            {
                if (maxUp.position.y > transform.position.y)
                {
                    rb.velocity = new Vector2(0, Time.deltaTime * 100 * speed);
                }
                else
                {
                    isGoingDown = true;
                }
            }
        }
    }

    public void FastPressDown()
    {
        isGoingDownFast = true;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(
            new Vector2(maxUp.position.x - 6, maxUp.position.y + 12),
            new Vector2(maxUp.position.x + 6, maxUp.position.y + 12),
            Color.red);
        Debug.DrawLine(
            new Vector2(maxUp.position.x - 6, maxUp.position.y + 12),
            new Vector2(maxUp.position.x - 6, maxDown.position.y - 12),
            Color.red);
        Debug.DrawLine(
            new Vector2(maxUp.position.x - 6, maxDown.position.y - 12),
            new Vector2(maxUp.position.x + 6, maxDown.position.y - 12),
            Color.red);
        Debug.DrawLine(
            new Vector2(maxUp.position.x + 6, maxUp.position.y + 12),
            new Vector2(maxUp.position.x + 6, maxDown.position.y - 12),
            Color.red);
    }
}
