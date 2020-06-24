using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Fox_Move : MonoBehaviour {

    public float walkingSpeed, runSpeed, jumpForce,cooldownHit;
	public bool walking, running, flyingUp, fallingDown, jumpWasPressedAndIsPossible, jumping, crouching, dead, attacking, usingSpecialAttack;
    private Rigidbody2D rb;
    private Animator anim;
	private SpriteRenderer sp;
	private float rateOfHit;
	private GameObject[] life;
	private int qtdLife;

    private float horizontalMoveInput, direction;
    private float moveSpeedWhileInAir = 0f;

    private BoxCollider2D playerCollider;

    private bool movingLeftDisabled, movingRightDisabled;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
		sp=GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<BoxCollider2D>();
        walking = false;
		running=false;
		flyingUp=false;
		fallingDown=false;
		jumpWasPressedAndIsPossible=false;
        jumping = false;
		crouching=false;
		rateOfHit=Time.time;
		life=GameObject.FindGameObjectsWithTag("Life");
		qtdLife=life.Length;
	}


    public void Update()
    {
        if(dead == false)
        {
            MovementInput();
            JumpingInput();
            Attack();
            Crouch();
            Special();

        }
    }

    private void JumpingInput()
    {
        if (Input.GetKeyDown(KeyCode.X) && !jumping)
        {
            if (Mathf.Abs(rb.velocity.y) < 2)
            {
                if (running)
                {
                    moveSpeedWhileInAir = runSpeed;
                }
                else
                {
                    moveSpeedWhileInAir = walkingSpeed;
                }
                jumpWasPressedAndIsPossible = true;
            }
            
        }
    }

    private void MovementInput()
    {
        if(!attacking && !crouching && !usingSpecialAttack)
        {
            horizontalMoveInput = Input.GetAxisRaw("Horizontal");
            if (horizontalMoveInput != 0)
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    //Run
                    walking = false; running = true;
                }
                else
                {
                    // Walk
                    walking = true; running = false;
                }
            }
            else
            {
                walking = false; running = false;
                // If no arrow is pressed to move then disable player's velocity immediately
                Vector3 noHorizontalMoveVel = new Vector2(0, rb.velocity.y);
                rb.velocity = noHorizontalMoveVel;
            }
        } 
    }

    // Update is called once per frame
    void FixedUpdate () {

        //if (dead == false)
        //{
        //    //Character doesnt choose direction in Jump									//If you want to choose direction in jump
        //    if (attacking == false)
        //    {                                                   //just delete the (jumping==false)
        //        if (jumping == false && crouching == false)
        //        {
        //            Movement();
        //            Attack();
        //            Special();
        //        }
        //        Jump();
        //        Crouch();
        //    }
        //    Dead();
        //}

        Movement();
        Jump();

    }

	void Movement(){
        // Character Move
        if (movingLeftDisabled && horizontalMoveInput < 0)
        {
            horizontalMoveInput = 0;
        }
        if (movingRightDisabled && horizontalMoveInput > 0)
        {
            horizontalMoveInput = 0;
        }

        if (jumping)
        {
            rb.velocity = new Vector2(horizontalMoveInput * moveSpeedWhileInAir * Time.deltaTime, rb.velocity.y);
        }
		else if(running)
        {
			//Run
			rb.velocity = new Vector2(horizontalMoveInput*runSpeed*Time.deltaTime,rb.velocity.y);
		}
        else if (walking)
        {
			//Walk
			rb.velocity = new Vector2(horizontalMoveInput*walkingSpeed*Time.deltaTime,rb.velocity.y);
		}

		//Turn
		if(rb.velocity.x < 0 && !attacking)
        {
			sp.flipX=true;
            anim.SetBool("LookingLeft", true);
		}
        else if(rb.velocity.x > 0 && !attacking)
        {
			sp.flipX=false;
            anim.SetBool("LookingLeft", false);
        }

		// Walking Animation
		if(rb.velocity.x != 0 && walking == true)
        {
			anim.SetBool("Walking", true);
		}
        else
        {
			anim.SetBool("Walking",false);
		}

        // Running Animation
        if (rb.velocity.x != 0 && running == true)
        {
			anim.SetBool("Running",true);
		}
        else
        {
			anim.SetBool("Running",false);
		}
	}

	void Jump(){
		// Applying jump force
		if(jumpWasPressedAndIsPossible)
        {
			rb.AddForce(new Vector2(0,jumpForce));
            jumpWasPressedAndIsPossible = false;
            jumping = true;
            crouching = false;
            rb.gravityScale = 1;
		}
        // Flying up Animation
        if (rb.velocity.y > 2)
        {
            flyingUp = true; fallingDown = false;
            anim.SetTrigger("Up");
        }
        // Falling animation
        else if ((flyingUp && rb.velocity.y <= 0) || rb.velocity.y < -2)
        {
            flyingUp = false; fallingDown = true;
            anim.SetTrigger("Down");
        }
        
    }

	void Attack(){                                                              
        if (!jumping && !attacking && !usingSpecialAttack)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Vector3 noHorizontalMoveVel = new Vector2(0, rb.velocity.y);
                rb.velocity = noHorizontalMoveVel;
                anim.SetTrigger("Attack");
                attacking = true;

                walking = false; running = false;
            }
        }
		
	}

	void AttackEnd()
    {
		attacking=false;
	}

	void Special(){
        if (Input.GetKey(KeyCode.Space) && !jumping && !attacking)
        {
            walking = false; running = false; rb.velocity = Vector2.zero;
            usingSpecialAttack = true;
            anim.SetBool("Special", true);
        }
        else
        {
            usingSpecialAttack = false;
            anim.SetBool("Special", false);
        }
    }

	void Crouch(){
        //Crouch
        if(attacking == false && jumping == false)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                walking = false; running = false; rb.velocity = Vector2.zero;
                crouching = true;
                anim.SetBool("Crouching", true);
            }
            else
            {
                crouching = false;
                anim.SetBool("Crouching", false);
            }
        }
        
    }

	void OnTriggerEnter2D(Collider2D other){							//Case of Bullet
		if(other.tag=="Enemy"){
			anim.SetTrigger("Damage");
			Hurt();
		}
	}								

	void OnCollisionEnter2D(Collision2D other) {						//Case of Touch
		if(other.gameObject.tag=="Enemy"){
			anim.SetTrigger("Damage");
			Hurt();
		}
        else if ((fallingDown) && other.gameObject.tag == "Ground")
        {
            fallingDown = false; flyingUp = false; jumping = false;
            anim.SetTrigger("Ground");
            rb.gravityScale = 3;
        }
	}

    public void OnCollisionStay2D(Collision2D collision)
    {
        Vector3 contactPoint = collision.contacts[0].point;
        Vector3 playerColliderCenter = playerCollider.bounds.center;

        bool collisionFromPlayersRight = contactPoint.x > playerColliderCenter.x;
        bool collisionFromPlayersLeft = contactPoint.x < playerColliderCenter.x;

        
        if (!attacking && !usingSpecialAttack && !crouching && (walking || running || jumping))
        {
            // If there is collsion from left of player ...
            if (collisionFromPlayersLeft)
            {
                // If user wants to go left - towards collision ...
                Debug.Log("LEFT");
                if (horizontalMoveInput < 0)
                {
                    // If player is not moving because of pushing against collision ...
                    if (rb.velocity.x > -0.01)
                    {
                        // ... stop him from pushing on that collision, because he now floats in the air
                        horizontalMoveInput = 0;
                        movingLeftDisabled = true; movingRightDisabled = false;
                    }
                }
            }
            // Same as above if statement
            if (collisionFromPlayersRight)
            {
                Debug.Log("RIGHT");
                if (horizontalMoveInput > 0)
                {
                    if (rb.velocity.x < 0.01)
                    {
                        horizontalMoveInput = 0;
                        movingRightDisabled = true; movingLeftDisabled = false;
                    }
                }
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        movingLeftDisabled = false; movingRightDisabled = false;
    }

    void Hurt(){
		if(rateOfHit<Time.time){
			rateOfHit=Time.time+cooldownHit;
			Destroy(life[qtdLife-1]);
			qtdLife-=1;
		}
	}

	void Dead(){
		if(qtdLife<=0){
			anim.SetTrigger("Dead");
			dead=true;

		}
	}

	public void TryAgain(){														//Just to Call the level again
		SceneManager.LoadScene(0);
	}
}
