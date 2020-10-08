using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody rb;

    private float jumpForce = 200f;
    [SerializeField] int maxJump = 0;
    private bool jumped;

    //Gets the rigidbody component before the first frame
    private void Awake() => rb = GetComponent<Rigidbody>();

    //Checks if the player has already jumped or not
    void Update()
    {
        if(jumped == false)
        {
            jumped = Input.GetButtonDown("Jump");
        }   
    }

    //Checks if the player is grounded. If so, the player gets force in the Y direction performing a jump
    private void FixedUpdate()
    {
        if(jumped && maxJump == 0)
        {
            rb.AddForce(jumpForce * Vector3.up);
            maxJump++;
        }
    }

    //The planes labelled ground are the "isGrounded" states, so once the player jumps and lands, they can jump again
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            maxJump = 0;
            jumped = false;
        }
    }
}
