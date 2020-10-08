using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UpDownPlatform : MonoBehaviour
{
    public double minHeight;
    public double maxHeight;
    public float speed;

    [SerializeField] private bool isTop, isLow;

    private void Update()
    {
        Move();
    }

    /*
     * Checks to see if the object is moving up or down from booleans isTop and isLow
     * once the objects reach the max and minimum heights, they switch states moving the opposite direction
     */
    private void Move()
    {
        if(isTop)
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
        else if(isLow)
        {
            transform.position += -transform.up * speed * Time.deltaTime;
        }

        if(transform.position.y <= minHeight && !isTop)
        {
            isLow = false;
            isTop = true;
        }else if(transform.position.y >= maxHeight && !isLow)
        {
            isTop = false;
            isLow = true;
        }
    }
}
