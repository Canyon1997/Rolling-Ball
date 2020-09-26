using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UpDownPlatform : MonoBehaviour
{
    public double minHeight;
    public double maxHeight;
    public float speed;

    bool isTop, isLow;

    private void Update()
    {
        Move();
        if(isTop && isLow)
        {
            Debug.Log("Both can't be true at the same time!");
        }
    }

    private void Move()
    {
        if(isLow)
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
        else if(isTop)
        {
            transform.position += -transform.up * speed * Time.deltaTime;
        }

        if(transform.position.y >= maxHeight && !isLow)
        {
            isTop = false;
            isLow = true;
        }else if(transform.position.y <= minHeight && !isTop)
        {
            isLow = false;
            isTop = true;
        }
    }
}
