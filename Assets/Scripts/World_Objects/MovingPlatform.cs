using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public int maxRightLength = 25;
    public int maxLeftLength = -25;
    public float speed;

    public bool isRight, isLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        MoveDirection();

        if(isRight && isLeft)
        {
            Debug.LogError("Both can not be true at the same time!");
        }
    }

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

    void Move()
    {
        if (isRight) //moves object to the right
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }

        if (isLeft) //moves object to the left
        {
            transform.position += -transform.right * speed * Time.deltaTime;
        }
    }


}
