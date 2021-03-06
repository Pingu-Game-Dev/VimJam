﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool hasJumped = false;


    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * 10f * runSpeed;

        if (Input.GetButtonDown("Jump")){
            jump = true;
        }
        
        if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical")==1)
        {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch")){
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch")){
            crouch = false;
        }

    }

    void FixedUpdate(){
        
        if (hasJumped == true)
        {
            jump = false;
            hasJumped = false;
        }
        if (jump == true)
        {
            hasJumped = true;
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        
    }

}
