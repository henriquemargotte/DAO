using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;   //Intância singleton do Player Manager

    const int maxBodys = 1;

    public Transform player;
    public float deathCoolDown;
    public float effectCoolDown;
    public Transform checkPoint;
    public GameObject deadBody;
    public GameObject deathEffect;

    private GameObject[] bodyList = new GameObject[maxBodys];
    private int deathCounter = 0;
    private float lastDeath;
    private float lastEffect;

    private void Awake(){
        if(instance != null){
            Destroy(gameObject);
        }
        else{
            instance = this;
        }
    }

    void Start(){
        lastDeath = Time.time - deathCoolDown;
        lastEffect = Time.time - effectCoolDown;
    }

    //Função para acessar o Player Manager de fora da classe
    public static PlayerManager GetInstance(){
		return instance;
	}

    public int GetDeathCount(){
        return deathCounter;
    }

    
    //Volta para o Checkpoint
    public void Reset(){
        player.position = checkPoint.position;
    }

    //Cria um deadBody e volta para o Checkpoint
    public void Kill(){
        if(Time.time - lastDeath > deathCoolDown){

            int index = deathCounter % maxBodys;
        
            if(bodyList[index] != null){
                Destroy(bodyList[index]);
            }
        
            bodyList[index] = (Instantiate(deadBody, player.position, Quaternion.identity));
            Instantiate(deathEffect, player.position,Quaternion.identity);

            deathCounter++;
            Reset();
            StartCoroutine(DisablePlayer(deathCoolDown, index));
            lastDeath = Time.time;
        }
    }

    //Exclui todos os deadBody's
    public void Clear(){
        for(int i = 0; i < maxBodys; i++){
            if(bodyList[i] != null){
                Destroy(bodyList[i]);
                bodyList[i] = null;
            }
        }
    }

    public void PlayEffect(Vector3 effectPosition){
        if(Time.time - lastEffect > effectCoolDown){
            Instantiate(deathEffect, effectPosition, Quaternion.identity);
            lastEffect = Time.time;
        }
    }

    IEnumerator DisablePlayer(float t, int i){
        Player playerScript = player.gameObject.GetComponent<Player>();
        Rigidbody2D rgbd2d = player.gameObject.GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<dg_simpleCamFollow>().target = bodyList[i].transform;
        
        playerScript.Death(true);
        rgbd2d.constraints = RigidbodyConstraints2D.FreezeAll;
        
        yield return new WaitForSeconds(t);

        Camera.main.GetComponent<dg_simpleCamFollow>().target = player;
        rgbd2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerScript.Death(false);
    }
}
