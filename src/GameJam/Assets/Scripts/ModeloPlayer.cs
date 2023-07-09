using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;

public class ModeloPlayer : MonoBehaviour{
    
    public float jumpForce, walkSpeed, flySpeed, timer, maxTime;
    public bool fly, floating;
    public Transform feetPosition;
    public LayerMask ground;
    public int[] points = new int[2];
    
    private Rigidbody2D r2d2;
    private bool isGrounded;
    private float direction;
    private int quantFloat;
    private Vector3 startPosition;

    void Start(){
        r2d2 = GetComponent<Rigidbody2D>();
        startPosition = gameObject.transform.position;
    }
    void Update(){
        timer += Time.deltaTime;
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, 0.3f, ground);
        //direction = CrossPlatformInputManager.GetAxis("Horizontal");

        Move();

        if(timer >= maxTime){
            GameOver();
        }  
    }
    public void Jump(){
        if(isGrounded || fly || (floating && quantFloat > 0)){
            r2d2.velocity = new Vector2(r2d2.velocity.x, jumpForce);
            quantFloat -= 1;
        }
    }
    public void GetDirection(float dir){
        direction = dir;
    }
    void Move(){
        if(isGrounded){
            r2d2.velocity = new Vector2(direction * walkSpeed, r2d2.velocity.y);
            quantFloat = 1;
        }
        else
            r2d2.velocity = new Vector2(direction * flySpeed, r2d2.velocity.y);

        if(direction != 0)
            gameObject.transform.localScale = new Vector3(direction,2,1);
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Point1")){
            points[0] += 1;
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Point2")){
            points[1] += 1;
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("End")){
            Win();
        }
        if(other.gameObject.CompareTag("Recall")){
            r2d2.velocity = new Vector3(0,0,0);
            gameObject.transform.position = startPosition;
        }
    }

    void GameOver(){
        print("Game Over");
        //ChangeScene.LoadSceneStatic("GameOver"+GameController.fase);
    }
    void Win(){
        print("Win");
    }
}
