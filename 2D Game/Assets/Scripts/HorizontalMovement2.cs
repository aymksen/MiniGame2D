using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement2 : Character2
{
    public float speed;
    public float distanceToCollider;
    public LayerMask collisionLayer;

    private float horizontalInput;

    protected override void Initializtion()
    {
        base.Initializtion();
    }

    // Update is called once per frame 
    void Update()
    {
        if (Input.GetAxis("Horizontal2") != 0)
        {
            horizontalInput = Input.GetAxis("Horizontal2");
        }
        else
        {
            horizontalInput = 0;
        }


        if (Input.GetKey(KeyCode.RightArrow))
        {
            
            transform.eulerAngles = new Vector2(0, 0);
        }
        //Move Left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            
            transform.eulerAngles = new Vector2(0, 180); //flip the character on its x axis
        }


    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * speed * Time.deltaTime, rb.velocity.y);
        SpeedModifier();
    }

    private void SpeedModifier()
    {
        if((rb.velocity.x > 0 && CollisionCheck(Vector2.right, distanceToCollider, collisionLayer)) || (rb.velocity.x < 0 && CollisionCheck(Vector2.left, distanceToCollider, collisionLayer)))
        {
            rb.velocity = new Vector2(.01f, rb.velocity.y);
        }
    }
}
