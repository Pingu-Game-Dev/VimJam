using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public GameObject music;
    private Component[] sources;
    private GameObject player;
    // Start is called before the first frame update

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetType() == new BoxCollider2D().GetType()){
            sources = music.GetComponentsInChildren<AudioSource>();
            player = collision.collider.gameObject;

            foreach (AudioSource source in sources){
                source.enabled = false;
            }

            player.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
