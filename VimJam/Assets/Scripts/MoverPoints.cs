using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class MoverPoints : MonoBehaviour
{
    // Setup bits
    [SerializeField] private Vector2[] Points = new Vector2[4];
    [SerializeField] private float m_MoveSpeed = 4f; // dictates the fastest x or y speed allowed
    [SerializeField] private int StartingIndex = 0;
    private Vector2 startPos;
    private float xlast, ylast, xspeed, yspeed, xdiff, ydiff;
    private int xdir, ydir; // will be 1 or -1 depending on direction of travel, or 0 if not moving
    private int target;


    void Start()
    {
        startPos = gameObject.transform.position;
        xlast = startPos.x;
        ylast = startPos.y;
        target = StartingIndex;

        // work out the new direcion
        // for x
        if (Points[target].x > xlast)
        {
            xdir = 1;
        }
        else if (Points[target].x == xlast)
        {
            xdir = 0;
        }
        else
        {
            xdir = -1;
        }

        // and for y
        if (Points[target].y > ylast)
        {
            ydir = 1;
        }
        else if (Points[target].y == ylast)
        {
            ydir = 0;
        }
        else
        {
            ydir = -1;
        }

        // find the speed
        // start by finding the distance it has to go
        xdiff = (Points[target].x - xlast) * xdir;
        ydiff = (Points[target].y - ylast) * ydir;

        if (xdiff > ydiff)
        {
            xspeed = m_MoveSpeed *xdir;
            yspeed = m_MoveSpeed * (ydiff / xdiff) * ydir;
        }
        else
        {
            yspeed = m_MoveSpeed * ydir;
            xspeed = m_MoveSpeed * (xdiff / ydiff);
        }

    }



    // Update is called once per frame
    void Update()
    {
        
        // check if it has arrived, and if so, then move on to the next one
        if ((transform.position.x * xdir >= Points[target].x * xdir) & (transform.position.y * ydir >= Points[target].y * ydir))
        {
            
            // move precicely to the place it was supposed to go
            transform.position = new Vector2(Points[target].x, Points[target].y);

            // reset xlast and ylast to the current location
            xlast = Points[target].x;
            ylast = Points[target].y;

            // increment target, and wrap if necissary 
            target++;
            if (target >= Points.Length)
            {
                target = 0;
            }

            // work out the new direcion
            // for x
            if (Points[target].x > xlast)
            {
                xdir = 1;
            } else if (Points[target].x == xlast)
            {
                xdir = 0;
            } else
            {
                xdir = -1;
            }

            // and for y
            if (Points[target].y > ylast)
            {
                ydir = 1;
            }
            else if (Points[target].y == ylast)
            {
                ydir = 0;
            }
            else
            {
                ydir = -1;
            }

            // find the speed
            // start by finding the distance it has to go
            xdiff = (Points[target].x - xlast) * xdir;
            ydiff = (Points[target].y - ylast) * ydir;

            if (xdiff > ydiff)
            {
                xspeed = m_MoveSpeed * xdir;
                yspeed = m_MoveSpeed * (ydiff / xdiff) * ydir;
            } else
            {
                yspeed = m_MoveSpeed * ydir;
                xspeed = m_MoveSpeed *(xdiff / ydiff) * xdir;
            }

        }



        // finally, do the actual movement
        transform.position = new Vector2(transform.position.x + xspeed * Time.deltaTime, transform.position.y + yspeed * Time.deltaTime);
        

    }
}
