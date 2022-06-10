using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement1 : Character1
{
    public float speed;
    public float distanceToCollider;
    public LayerMask collisionLayer;
    public Animator animator11;
    
    private float horizontalInput;
    public Joystick joystick;
    private int joymove;
    

    protected override void Initializtion()
    {
        base.Initializtion();
    }

    // Update is called once per frame 
    void Update()
    {
        if (joystick.Horizontal > 0)
        {
            joymove = 1;
        }
        else if (joystick.Horizontal < 0)
        {
            joymove = -1;
        }
        else joymove = 0;

        animator11.SetFloat("speed", Mathf.Abs(horizontalInput));

        if (Input.GetAxis("Horizontal1") != 0 || joystick.Horizontal !=0)
        {
            horizontalInput = Input.GetAxis("Horizontal1")+ joymove;
            




        }
        else
        {
            horizontalInput = 0;
        }



        if (Input.GetKey(KeyCode.D) || joystick.Horizontal>0)
        {
            
            transform.eulerAngles = new Vector2(0, 0);
        }
        
        //Move Left
        if (Input.GetKey(KeyCode.A) || joystick.Horizontal < 0)
        {
            
            transform.eulerAngles = new Vector2(0, 180); //flip the character on its x axis
        }
        if (Input.GetKey(KeyCode.L))
        {

            transform.position = new Vector3(-8, 77, -4);
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
