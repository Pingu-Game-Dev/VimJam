using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    private Rigidbody2D player;
    public Transform groundCheck;
    public float deathVel = -50f;
    bool ground = true;
    bool flag = false;
    float minVel = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, .15f);
        for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				flag = true;
			}
		}
        if (flag) {ground = true;}
        else {ground = false;}

        flag = false;
    }

    void FixedUpdate()
    {
        if (!ground && player.velocity.y < minVel) {
            minVel = player.velocity.y;
        }

        if (ground && minVel < 0f){
            if (minVel < deathVel){
                player.transform.position = Death.respawnPoint;
                minVel = 0f;
            }
        }
    }
}
