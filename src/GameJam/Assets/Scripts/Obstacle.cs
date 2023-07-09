using UnityEngine;

public class Obstacle : MonoBehaviour
{
    
    public string playerTag;
    public bool jump; //true = trampolim, false = espinhos
    public float jumpForce;

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.tag.Equals(playerTag)){
            if(jump){
                collision.gameObject.GetComponent<Player>().Jump(jumpForce);
            }
            else
                PlayerManager.GetInstance().Kill();
        }

    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.transform.tag.Equals(playerTag)){
            PlayerManager.GetInstance().PlayEffect(collider.transform.position);
        }
    }
}
