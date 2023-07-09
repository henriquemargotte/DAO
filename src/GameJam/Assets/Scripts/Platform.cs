using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Platform : MonoBehaviour
{

    const float minAlpha = 0.03f;

    public class MaskFade{//Classe para armazenar a Mask e alphaCutoff
        //propriedades
        public SpriteMask mask {get; set;}
        public float t {get; set;}
        private int fade;

        //métodos
        public MaskFade(SpriteMask m){
            mask = m;
            t = 1;
            fade = -1;
        }

        public bool Fade(float value){
            t += value * fade;
            mask.alphaCutoff = t;
            if (t < minAlpha){
                fade *= -1;
            }
            return t > 1;
        }
    }

    public GameObject maskObject;                   //mask que será instanciada
    public bool hideSprite = true;
    [Range(0,1)]public float sizeMultiplier = 0.1f; //Tamanho da mask de acordo com a velocidade de impacto
    [SerializeField] float fadeMultiplier = 3.5f;

    List<MaskFade> maskList = new List<MaskFade>(); //armazena as masks

    void Start(){
        if(hideSprite){
            SpriteRenderer sprtRenderer = GetComponent<SpriteRenderer>();
            if(sprtRenderer == null){
                GetComponent<TilemapRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }else{
                sprtRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }
        }
    }

    void FixedUpdate(){
        List<MaskFade> toRemove = new List<MaskFade>();

        foreach (MaskFade maskf in maskList)
        {
            if(maskf.Fade(fadeMultiplier * Time.fixedDeltaTime)){
                toRemove.Add(maskf);
            }
        }

        foreach (MaskFade maskf in toRemove)
        {
            maskList.Remove(maskf);
            Destroy(maskf.mask.gameObject);   
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        
        //Para cada ponto de contato iremos criar uma mask
        foreach (ContactPoint2D contact in collision.contacts)
        {
            GameObject obj = Instantiate(maskObject,contact.point,Quaternion.identity);
            float scale = collision.relativeVelocity.magnitude * sizeMultiplier;

            //O tamanho da mask depende da velocidadedo impacto
            obj.transform.localScale = new Vector3(scale, scale, scale);
            maskList.Add(new MaskFade(obj.GetComponent<SpriteMask>()));
        }
    }
}
