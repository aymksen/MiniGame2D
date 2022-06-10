using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class Jump1 : Character1
{
    
    public int maxJumps;
    public float jumpForce;
    public float maxButtonHoldTime;
    public float holdForce;
    public float distanceToCollider;
    public float maxJumpSpeed;
    public float minJumpSpeed;
    public float maxFallSpeed;
    public float fallSpeed;
    public float gravityMultipler;
    [HideInInspector] public float jumpCoolDown;
    [HideInInspector] public float dmgCoolDown;
    [HideInInspector] public float jumpCoolDown2;
    public LayerMask collisionLayer;
    

    private bool jumpPressed;
    private bool jumpHeld;
    private float buttonHoldTime;
    private float originalGravity;
    private bool kaydor;
    [SerializeField] private int numberOfJumpsLeft;

    [Header("For WallSliding")]
    [SerializeField] float wallSlideSpeed;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] Vector2 wallCheckSize;
    [SerializeField] private bool isTouchingWall;
    [SerializeField] private bool isWallSliding;
    public Animator animator11;

    [SerializeField] LayerMask spikeLayer;
    [SerializeField] public bool isTouchingspike;
    [SerializeField] public bool damaged;
    private Heart Heart;
    

    public Joystick joystick;
    public TouchPhase joybutton;
    public bool touche;
    public bool touchedown;

    [Header("Bash")]
    [SerializeField] private float Raduis;
    [SerializeField] GameObject BashAbleObj;
    private bool NearToBashAbleObj;
    private bool IsChosingDir;
    private bool IsBashing;
    [SerializeField] private float BashPower;
    [SerializeField] private float BashTime;
    [SerializeField] private float TimeToBash;
    [SerializeField] private GameObject Arrow;
    Vector3 BashDir;
    private float BashTimeReset;
    private float nextBash=0;











    protected override void Initializtion()
    {
        
        base.Initializtion();
        buttonHoldTime = maxButtonHoldTime;
        originalGravity = rb.gravityScale;
        numberOfJumpsLeft = maxJumps;
        Heart = GameObject.Find("Player-sprite (1)").GetComponent<Heart>();
        BashTimeReset = BashTime;
   
    }
    public void ScaleToTarget(Vector3 targetScale, float duration)
    {
        StartCoroutine(ScaleToTargetCoroutine(targetScale, duration));
    }

    private IEnumerator ScaleToTargetCoroutine(Vector3 targetScale, float duration)
    {
        Vector3 startScale = Arrow.transform.localScale;
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            //smoother step algorithm
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            Arrow.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        yield return null;
    }
    public void ScaleToTarget1(Vector3 targetScale, float duration)
    {
        StartCoroutine(ScaleToTargetCoroutine1(targetScale, duration));
    }

    private IEnumerator ScaleToTargetCoroutine1(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            //smoother step algorithm
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        yield return null;
    }
    void Bash()
    {
        RaycastHit2D[] Rays = Physics2D.CircleCastAll(transform.position, Raduis, Vector3.forward);
        foreach(RaycastHit2D ray in Rays)
        {
            NearToBashAbleObj = false;
            if (ray.collider.tag == "Bash")
            {
                NearToBashAbleObj = true;
                BashAbleObj = ray.collider.transform.gameObject;
                break;
            }
        }
        if (NearToBashAbleObj)
        {
            //Arrow.transform.localScale = new Vector2(.1f, .1f);
            BashAbleObj.GetComponent<SpriteRenderer>().color = Color.yellow;
            if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time > nextBash && NearToBashAbleObj)
            {
                
                nextBash = Time.time + TimeToBash;
                Time.timeScale = 0.1f ;
                //BashAbleObj.transform.localScale = new Vector2(0.4f, 0.4f);
                Arrow.SetActive(true);
                Arrow.transform.position = BashAbleObj.transform.transform.position;
                Arrow.transform.localScale = new Vector3(0f, 0f);
                ScaleToTarget(new Vector3(.8f, 1.2f, 1f), .05f);
                ScaleToTarget1(new Vector3(1f, 1f, 1f), 0.1f);
                //Arrow.transform.localScale += new Vector3(1, 1) * Time.deltaTime ;
                IsChosingDir = true;


            }
            else if (IsChosingDir && Input.GetKeyUp(KeyCode.Mouse1))
            {
                
                Time.timeScale = 1f ;
                //BashAbleObj.transform.localScale = new Vector2(1, 1);
                IsChosingDir = false;
                IsBashing = true;
                rb.velocity = Vector2.zero;
                transform.position = BashAbleObj.transform.position; // Inside Bashable
                BashDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                BashDir.z = 0;
                if (BashDir.x > 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);

                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                BashDir = BashDir.normalized;
                BashAbleObj.GetComponent<Rigidbody2D>().AddForce(-BashDir * 25, ForceMode2D.Impulse);
                Arrow.SetActive(false);
                transform.localScale = new Vector3(6.7f, 5.7f);


            }
        }
        else if (BashAbleObj != null && IsChosingDir)
        {
            
            Time.timeScale = 1f;
            //BashAbleObj.transform.localScale = new Vector2(1, 1);
            IsChosingDir = false;
            IsBashing = true;
            rb.velocity = Vector2.zero;
            transform.position = BashAbleObj.transform.position; // Inside Bashable
            BashDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            BashDir.z = 0;
            if (BashDir.x > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);

            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            BashDir = BashDir.normalized;
            BashAbleObj.GetComponent<Rigidbody2D>().AddForce(-BashDir * 25, ForceMode2D.Impulse);
            Arrow.SetActive(false);
            BashAbleObj.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else if (BashAbleObj != null )
        {
            
            Time.timeScale = 1f;
            BashAbleObj.GetComponent<SpriteRenderer>().color = Color.white;
            transform.localScale = new Vector3(6.7f, 5.7f);
        }
         
        if (IsBashing)
        {
            transform.localScale = new Vector3(6.7f, 5.7f);
            if (BashTime > 0 )
            {
                
                BashTime -= Time.deltaTime;
                //rb.velocity = BashDir * BashPower*10 * Time.deltaTime;
                rb.AddForce(BashDir * BashPower* Time.deltaTime);
                numberOfJumpsLeft = 1;
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            else
            {
                IsBashing = false;
                BashTime = BashTimeReset;
                

                Falling();
            }
        }
         

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Raduis);
    }

    void WallSlide()
    {
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0 || isTouchingWall && wallSlideSpeed == -20)
        {
            isWallSliding = true;
            
        }
        else
        {
            
            isWallSliding = false;
        }
        if (isWallSliding /*&& Input.GetKey(KeyCode.LeftShift)*/)
        {
            
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
    }
    private void Update()
    {
        
        Inputs();
        CheckForJump();
        GroundCheck();
        Bash();
        //spikes();


        if (Input.GetKey(KeyCode.W) || joystick.Vertical > 0)
        {

            wallSlideSpeed = -20;
        }
        else if (Input.GetKey(KeyCode.S) || joystick.Vertical < 0)
        {

            wallSlideSpeed = 7;
        }
        else wallSlideSpeed = 0;


        isTouchingWall = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, wallLayer);

       
            int i = 0;
            while(i < Input.touchCount)
            {
                Touch trr = Input.GetTouch(i);

                if ((trr.position.x > Screen.width / 2) && trr.phase == TouchPhase.Began)
                {
                        touche = true;
                }
                    else { touche = false; }
                if (trr.position.x > Screen.width / 2 )
                {
                        touchedown = true;
                }

                if (trr.position.x > Screen.width / 2 && trr.phase == TouchPhase.Ended )
                {
                    
                    touchedown = false;            
                }
            ++i;
            }
    }


    
    void Inputs()
    {

        if (Input.GetKeyDown(KeyCode.Space) || touche || Input.GetKeyDown(KeyCode.V))
        {
            //Debug.Log("9laaaaaaa");

            jumpPressed = true;
            if (numberOfJumpsLeft == 2 )
            {
                animator11.SetBool("isJamping", true);
                animator11.SetBool("double jump", false);
                kaydor = false;
            }
            else
            {
                animator11.SetBool("isJamping", false);
                animator11.SetBool("double jump", true);
                kaydor = true;
                
            }
            animator11.SetBool("isFalling", false);
        }
        else
            jumpPressed = false;
            kaydor = false;
        if (Input.GetKey(KeyCode.Space) || touchedown || Input.GetKey(KeyCode.V)) 
        {
            animator11.SetBool("isFalling", false);
            jumpHeld = true;
            kaydor = true;
        }
        else
            jumpHeld = false;
        kaydor = false;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);
    }
       
        
    

    private void FixedUpdate()
    {
        if (IsBashing == false)
        WallSlide();
        IsJumping();
        

    }

    private void CheckForJump()
    {
        if (jumpPressed)
        {
            if (!character.isGrounded && numberOfJumpsLeft == maxJumps)
            {
                character.isJumping = false;
                return;
            }
            numberOfJumpsLeft--;
            if (numberOfJumpsLeft >= 0)
            {
                rb.gravityScale = originalGravity;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                buttonHoldTime = maxButtonHoldTime;
                character.isJumping = true;
                
            }
        }
    }
   


    private void IsJumping()
    {
        if(character.isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce);
            AdditionalAir();
            if (rb.velocity.y < minJumpSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, minJumpSpeed);
                
            }
        }
        if (rb.velocity.y > maxJumpSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxJumpSpeed);
            
        }
       
        
        Falling();
    }
    
        
    
        private void AdditionalAir()
    {
        if (jumpHeld)
        {
            buttonHoldTime -= Time.deltaTime;
            if (buttonHoldTime <= 0)
            {
                buttonHoldTime = 0;
                character.isJumping = false;
                
            }
            else
                
            rb.AddForce(Vector2.up * holdForce);
           
        }
        else
        {
            character.isJumping = false;
        }
    }

    private void Falling()
    {
        if(!character.isJumping && rb.velocity.y < fallSpeed)
        {
            rb.gravityScale = gravityMultipler;
            


        }
        if(rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
            


        }
        
    }
    void OnCollisionEnter2D (Collision2D collision)//public void spikes()
    {
        if (Heart.health == 0)
        {
            //Debug.Log("Dead!");
        }

        if (collision.gameObject.tag == "spikes") //if (CollisionCheck(Vector2.down, distanceToCollider, spikeLayer))
        {
            Heart.Damage(1);
            isTouchingspike = true;
            
                rb.AddForce(new Vector2(100000f * Time.deltaTime, 120000f * Time.deltaTime));
                 //Debug.Log("X: " + transform.position);
            //Destroy(gameObject);

        }
            
            //Heart.Damage(1);
            //StartCoroutine(Knockback(1f,-6, transform.position));
        
        else
        {    
            isTouchingspike = false;

        }
    }
    public /*IEnumerator*/void TakeDamage(int amt)
    {
        
        amt = 1;
        if (isTouchingspike == true )
        {
            
            Heart.health-=amt;
            /*if (Input.GetKey(KeyCode.D))
            {

                rb.AddForce(new Vector2(8f, 2f));
            }
            if (Input.GetKey(KeyCode.A))
            {

                rb.AddForce(new Vector2(-8f, 2f));
            }
            */


            //yield return 0;// new WaitForseconds(0.5f);
        }

    }

    /*public IEnumerator Knockback(float knockdur, float knockbackpwr, Vector2 knockbakdir)
    {
        float timer = 0;
        while (knockdur > timer)
        {
            timer += Time.deltaTime;
            rb.AddForce(new Vector2(knockbakdir.x * -10 * Time.deltaTime, knockbakdir.y * knockbackpwr*8 * Time.deltaTime));

        }

        yield return 0;
        if (damaged == true)
        {
            Debug.Log("dmg");
        }
    }*/





    private void GroundCheck()
    {
        if (CollisionCheck(Vector2.down, distanceToCollider, collisionLayer) && !character.isJumping)
        {
            animator11.SetBool("isFalling", false);
            animator11.SetBool("isJamping", false);
            animator11.SetBool("double jump", false);
            animator11.SetBool("iswalling", false);
            character.isGrounded = true;
            numberOfJumpsLeft = maxJumps;
            rb.gravityScale = originalGravity;
            jumpCoolDown = Time.time + 0.05f;
        }
        else if (Time.time < jumpCoolDown)
        {
            isGrounded = true;
            numberOfJumpsLeft = maxJumps - 1;
            rb.gravityScale = originalGravity;

        }
        else if (isTouchingWall == true)
        {
            animator11.SetBool("iswalling", true);
            animator11.SetBool("isFalling", false);
            animator11.SetBool("double jump", false);
            animator11.SetBool("isJamping", false);
            numberOfJumpsLeft = maxJumps + 1;
            isGrounded = false;
            jumpCoolDown2 = Time.time + 0.05f;
        }
        /*else if (Time.time < jumpCoolDown2 && isTouchingWall == false)
        {

            numberOfJumpsLeft = 3;
            rb.gravityScale = originalGravity;
        }*/
            

         else
                 {
            character.isGrounded = false;
            if (!isTouchingWall && !jumpPressed && !jumpHeld && !kaydor )
            { 
            animator11.SetBool("isFalling", true);
            }   
            else { animator11.SetBool("isFalling", false); }
            animator11.SetBool("iswalling", false);
            animator11.SetBool("isJamping", true);


        }
    }
}
