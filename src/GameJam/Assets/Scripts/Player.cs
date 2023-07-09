using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    public float jumpForce, walkSpeed, flySpeed, dashSpeed, dashTime;
    //public int moreJumps; //Número de pulos a mais (double jump)
    public Transform feetPosition, grabPosition, grab2Position;
    public Animator anim;
    public LayerMask ground;

    private Rigidbody2D r2d2;
    private bool isGrounded, isGrabbing, isDashing;
    private float direction, dashTimer;
    private int dashCount;
    private Vector3 startPosition;

    void Start(){
        r2d2 = GetComponent<Rigidbody2D>();
        startPosition = gameObject.transform.position;
        isDashing = false;
    }

    void Update(){
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, 0.3f, ground);
        isGrabbing = (Physics2D.OverlapCircle(grabPosition.position, 0.3f, ground)
                    || Physics2D.OverlapCircle(grab2Position.position, 0.3f, ground))
                    && direction != 0 && (int)r2d2.velocity.y == 0 && !isGrounded;
        direction = Input.GetAxisRaw("Horizontal");

        if(isGrabbing || isGrounded)
            dashCount = 1;

        anim.SetInteger("velX",(int)r2d2.velocity.x);
        anim.SetInteger("velY",(int)r2d2.velocity.y);
        anim.SetInteger("dir",(int)direction);
        anim.SetBool("isGrabbing", isGrabbing);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", isDashing);


        Move();
        if(Input.GetButtonDown("Jump") && (isGrounded || isGrabbing)){ // || jumpN > 0)){
            Jump(jumpForce);
        //    jumpN -= 1;
        }
        if(Input.GetButtonDown("Fire3") && !isDashing && direction != 0 && dashCount > 0){
            isDashing = true;
            dashTimer = 0;
            anim.SetTrigger("dash");
        }
        if(isDashing){
            Dash();
        }
    }

    public void Jump(float force){
        dashCount = 1;
        anim.SetTrigger("jump");
        r2d2.velocity = new Vector2(r2d2.velocity.x, force);
    }

    void Move(){
        if(isGrounded){
            r2d2.velocity = new Vector2(direction * walkSpeed, r2d2.velocity.y);
            //jumpN = moreJumps;
        }
        else
            r2d2.velocity = new Vector2(direction * flySpeed, r2d2.velocity.y);

        if(direction != 0)
            gameObject.transform.localScale = new Vector3(direction,1,1);
    }

    void Dash(){
        dashCount = 0;
        r2d2.velocity = new Vector2(direction * dashSpeed, 0);
        dashTimer += Time.deltaTime;
        if(dashTimer >= dashTime)
            isDashing = false;
    }

    public void Death(bool idle){
        r2d2.velocity = new Vector2(0,0);
        anim.SetBool("idle", idle);
    }


}
