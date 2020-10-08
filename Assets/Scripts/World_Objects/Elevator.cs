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

    // Detects if the player landed on the elevator object
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            onMe = true;
        }
    }

    /*
     * Has a boolean, goDown, that states if the elevator should move downwards. If false, the elevator moves up
     * Checks to see if the onMe boolean is true and moves the elevator to the heightLimit adjusted in the inspector
     */
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
