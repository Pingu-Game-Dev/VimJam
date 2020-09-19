using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLoop : MonoBehaviour
{

    public AudioSource source;
    public float fadeTime = 3f;

    bool flag = false;
    float fadeInc = 0.1f;

    
    void OnCollisionEnter2D(Collision2D col){
        flag = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        // Set respawn point to the current location 
        Death.respawnPoint = gameObject.transform.position;
    }

    void Update(){
        if (flag){
            fadeTime -= Time.deltaTime;
            fadeInc -= Time.deltaTime;
        }
        if (fadeTime <= 0){
            Destroy(gameObject);
            flag = false;
        }
        else if (fadeInc <= 0){
            fadeInc = 0.1f;
            source.volume += fadeInc / fadeTime;
        }
    }

}
