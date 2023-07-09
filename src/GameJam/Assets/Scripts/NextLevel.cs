using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public string sceneName;
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.transform.tag.Equals("Player")){
            SceneChanger.GetInstance().ChangeScene(sceneName);
        }
    }
}
