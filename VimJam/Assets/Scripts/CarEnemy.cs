using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnemy : MonoBehaviour
{
    private float timer = 0f;
    private Vector3 scaleChange;
    private Vector3 startScale;
    public float speed = 3f;

    public float deathTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        startScale = gameObject.transform.localScale;
        scaleChange = new Vector3(speed *0.1f * Time.deltaTime, speed *0.1f * Time.deltaTime, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        gameObject.transform.localScale += scaleChange;
        //reset
        if (timer >= deathTime && gameObject.GetComponent<BoxCollider2D>().enabled){
            timer = 0f;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.transform.localScale = startScale;

        }

        //death
        if (timer >= speed){
            timer = 0f;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
 