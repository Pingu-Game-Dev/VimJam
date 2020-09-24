using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnemy : MonoBehaviour
{
    private float timer = 0f;
    public float speed = 3f;

    public float deathTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= deathTime && gameObject.GetComponent<BoxCollider2D>().enabled){
            timer = 0f;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (timer >= speed){
            timer = 0f;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
