using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class Death : MonoBehaviour {

    public static Vector2 respawnPoint;
    public GameObject player;

    private void Start()
    {
        respawnPoint = player.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.Equals(player)){
        collision.transform.position = respawnPoint;
        }
    }

}
