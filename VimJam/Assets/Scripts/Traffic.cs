using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic : MonoBehaviour
{

    public BoxCollider2D car;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (car.enabled){
            animator.SetBool("Stop", true);
        }
        else{
            animator.SetBool("Stop",false);
        }
    }
}
