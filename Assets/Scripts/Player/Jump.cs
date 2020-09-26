using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody rb;

    private float jumpForce = 200f;
    [SerializeField] int maxJump = 0;
    private bool jumped;

    private void Awake() => rb = GetComponent<Rigidbody>();

    void Update()
    {
        if(jumped == false)
        {
            jumped = Input.GetButtonDown("Jump");
        }   
    }

    private void FixedUpdate()
    {
        if(jumped && maxJump == 0)
        {
            rb.AddForce(jumpForce * Vector3.up);
            maxJump++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            maxJump = 0;
            jumped = false;
        }
    }
}
