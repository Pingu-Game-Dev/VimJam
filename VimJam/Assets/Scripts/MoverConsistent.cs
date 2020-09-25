using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class MoverConsistent : MonoBehaviour
{
    // Setup bits
    public float m_LeftBound = 4f;
    public float m_RightBound = 4f;
    public bool m_StartMovingRight = true;
    public float m_MoveSpeed = 4f;
    private Vector2 startPos;
    // Grid size
    private readonly float SCALE = 1f;


    void Start()
    {
        startPos = gameObject.GetComponent<Transform>().position;
        m_LeftBound *= SCALE;
        m_RightBound *= SCALE;
    }
   


    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > (m_RightBound + startPos.x))
        {
            m_StartMovingRight = false;
        }
        if (transform.position.x < (-m_LeftBound+startPos.x))
        {
            m_StartMovingRight = true;
        }

        if (m_StartMovingRight)
        {
            transform.position = new Vector2(transform.position.x + m_MoveSpeed * Time.deltaTime, transform.position.y);
        } else
        {
            transform.position = new Vector2(transform.position.x - m_MoveSpeed * Time.deltaTime, transform.position.y);
        }
        
    }
}
