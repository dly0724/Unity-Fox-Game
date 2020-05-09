using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform left, right;
    private bool Faceleft = true;
    public float Speed;
    private float leftx, rightx;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
       // leftx = left.position.x;
       // rightx = right.position.x;
       // Destroy(left.gameObjective);
        //Destroy(right.gameObjective);
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    void move()
    {
        if (Faceleft)
        {
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
            if (transform.position.x < left.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(Speed, rb.velocity.y);
            if (transform.position.x > right.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }

        }
    }
}
