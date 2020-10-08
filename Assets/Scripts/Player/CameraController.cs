using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;

    private Vector3 offset;


    /*
     * Locks cursor to the middle of the screen
     * Hides cursor on the first frame
     * Gets the difference in distance between the camera and player
     */
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        offset = transform.position - player.transform.position;
    }

    // Takes the current camera position and sets it to the player position plus the offset distance calculated on the first frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
