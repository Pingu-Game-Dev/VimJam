using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    Transform point;
    [SerializeField] private LayerMask m_WhatIsPlayer;
    public float bounceForce = 4000f;
    bool jump;
    Vector2 scale;
    public Rigidbody2D player;
    float Vel = 0f;
    float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        point = gameObject.transform;
        scale = new Vector2(gameObject.transform.localScale.x,gameObject.transform.localScale.y);
        jumpForce = player.GetComponent<CharacterController2D>().m_JumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")){
            jump = true;
        }
    }

    void FixedUpdate(){
        jump = false;
        Vel = player.velocity.y;
    }


    void OnCollisionExit2D(Collision2D collision){
        if (collision.collider.attachedRigidbody.Equals(player)){
            if (Vel >= 0f){
                    player.AddForce(new Vector2(0f,bounceForce));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.relativeVelocity.y < -20f){
            player.AddForce(new Vector2(0f,bounceForce + jumpForce));
        }
    }

}
