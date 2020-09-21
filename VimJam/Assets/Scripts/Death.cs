using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Death : MonoBehaviour {

    public static Vector2 respawnPoint = new Vector2(-50f,12f);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.position = respawnPoint;
    }

}
