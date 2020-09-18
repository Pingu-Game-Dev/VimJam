using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLoop : MonoBehaviour
{

    public AudioSource source;
    
    void OnCollisionEnter2D(Collision2D col){
        source.volume = 1f;
        Destroy(gameObject);
    }
}
