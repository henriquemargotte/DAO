using System.Collections;
using UnityEngine;

public class InterpolateSize : MonoBehaviour
{

    public int min;
    public int max;
    public float velocity;
    public bool destroyWhenFinished;
    public float destroyDelay;
    public bool objectInChild;

    float t = 0.0f;
    Transform obj;

    ParticleSystem particleSys;

    // Start is called before the first frame update
    void Start()
    {
        particleSys = GetComponent<ParticleSystem>();
        if(objectInChild){
            obj = transform.GetChild(0);
        }else{
            obj = transform;
        }
    }

    void FixedUpdate()
    {
        t += velocity * Time.fixedDeltaTime;
        float size = Mathf.Lerp(min,max, t);
        obj.localScale = new Vector3(size,size,size);
        
        if(destroyWhenFinished && (size >= max)){
            Destroy(gameObject, destroyDelay);
        }
    }
}
