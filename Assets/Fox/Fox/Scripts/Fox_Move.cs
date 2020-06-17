using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Fox_Move : MonoBehaviour {

    public float speed,jumpForce,cooldownHit;
	public bool walking, running,up,down,jumping,crouching,dead,attacking,special;
    private Rigidbody2D rb;
    private Animator anim;
	private SpriteRenderer sp;
	private float rateOfHit;
	private GameObject[] life;
	private int qtdLife;

    private float move = 0f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
		sp=GetComponent<SpriteRenderer>();
        walking = false;
		running=false;
		up=false;
		down=false;
		jumping=false;
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
        }
    }

    private void MovementInput()
    {
        move = Input.GetAxisRaw("Horizontal");
        if (move != 0)
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

    }

	void Movement(){
		//Character Move
		if(running)
        {
			//Run
			rb.velocity = new Vector2(move*speed*Time.deltaTime*3,rb.velocity.y);
		}
        else if (walking)
        {
			//Walk
			rb.velocity = new Vector2(move*speed*Time.deltaTime,rb.velocity.y);
		}

		//Turn
		if(rb.velocity.x < 0)
        {
			sp.flipX=true;
		}
        else if(rb.velocity.x > 0)
        {
			sp.flipX=false;
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
		//Jump
		if(Input.GetKeyDown(KeyCode.X)&&rb.velocity.y==0){
			rb.AddForce(new Vector2(0,jumpForce));

		}
		//Jump Animation
		if(rb.velocity.y>0&&up==false){
			up=true;
			jumping=true;
			anim.SetTrigger("Up");
		}else if(rb.velocity.y<0&&down==false){
			down=true;
			jumping=true;
			anim.SetTrigger("Down");
		}else if(rb.velocity.y==0&&(up==true||down==true)){
			up=false;
			down=false;
			jumping=false;
			anim.SetTrigger("Ground");
		}
	}

	void Attack(){																//I activated the attack animation and when the 
		//Atacking																//animation finish the event calls the AttackEnd()
		if(Input.GetKeyDown(KeyCode.C)){
			rb.velocity=new Vector2(0,0);
			anim.SetTrigger("Attack");
			attacking=true;
		}
	}

	void AttackEnd(){
		attacking=false;
	}

	void Special(){
		if(Input.GetKey(KeyCode.Space)){
			anim.SetBool("Special",true);
		}else{
			anim.SetBool("Special",false);
		}
	}

	void Crouch(){
		//Crouch
		if(Input.GetKey(KeyCode.DownArrow)){
			anim.SetBool("Crouching",true);
		}else{
			anim.SetBool("Crouching",false);
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
