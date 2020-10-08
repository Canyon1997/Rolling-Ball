using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LeftRightPlatform : MonoBehaviour
{
    public int maxRightLength = 25;
    public int maxLeftLength = -25;
    public float speed;

    [SerializeField] private bool isRight, isLeft;

    void Update()
    {
        Move();
        MoveDirection();
    }

    /*
     * states if the object should be moving to the left or right based on if the object has reached the min or max length
     * boolean stats will switch to which direction the object should be moving
     */
    void MoveDirection()
    {
        if(transform.position.x >= maxRightLength && !isLeft)
        {
            isRight = false;
            isLeft = true;
        }
        if(transform.position.x <= maxLeftLength && !isRight)
        {
            isLeft = false;
            isRight = true;
        }
    }

    //checks what boolean state the object is in and moves the object in that specific direction
    void Move()
    {
        if (isRight)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }

        if (isLeft)
        {
            transform.position += -transform.right * speed * Time.deltaTime;
        }
    }


}
