using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLoop : MonoBehaviour
{

    public AudioSource source;
    public float fadeTime = 3f;

    bool flag = false;
    float fadeInc = 0.1f;

    float circleRadius = 1f;
    Transform point;

    void Start(){
        point = gameObject.GetComponent<Transform>();
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

    void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, circleRadius);
        for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				flag = true;
				gameObject.GetComponent<SpriteRenderer>().enabled = false;
                Death.respawnPoint = new Vector2(point.position.x,point.position.y);
			}
		}
    }

}
