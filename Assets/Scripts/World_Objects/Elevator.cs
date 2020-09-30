using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private double heightLimit;
    public float speed;
    static bool onMe;
    [SerializeField] private bool goDown;

    void Update()
    {
            Elevating();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            onMe = true;
        }
    }

    void Elevating()
    {
        if(onMe && transform.position.y <= heightLimit)
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
        else if(onMe && goDown && transform.position.y >= heightLimit)
        {
            transform.position += -transform.up * speed * Time.deltaTime;
        }
    }
}
