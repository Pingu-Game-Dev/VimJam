using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Death : MonoBehaviour {

    public static Vector3 respawnPoint = new Vector3(-50f,12f);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.position = respawnPoint;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
