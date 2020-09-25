using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public GameObject music;
    public AudioSource closer;
    public string nextScene = "01 - Farm";
    private Component[] sources;
    private GameObject player;
    bool flag = false;
    // Start is called before the first frame update

    void Update()
    {
        if (closer.isPlaying && !flag)
        {
            flag = true;
        }
        else if (!closer.isPlaying && flag){
            SceneManager.LoadScene(nextScene);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetType() == new BoxCollider2D().GetType()){

            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            sources = music.GetComponentsInChildren<AudioSource>();
            player = collision.collider.gameObject;

        


            foreach (AudioSource source in sources){
                source.volume = 0f;
            }
            

            player.GetComponent<PlayerMovement>().enabled = false;
            closer.Play();
        }
    }
}
