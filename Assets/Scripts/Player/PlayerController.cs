using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 moving = Vector3.zero;

    [Header("Particles")]
    public ParticleSystem oneCollectable;
    public ParticleSystem twoCollectables;
    public ParticleSystem threeCollectables;

    public Text scoreText;
    public Text LoseText;
    public Text WinText;
    public Text Collectabletext;

    public float speed = 10f;

    private float maxSpeed;
    private int score = 0;

    bool gameFinished;
    bool isDead;

    //Gets rigidbody component from player before the first frame
    private void Awake() => rb = GetComponent<Rigidbody>();

    //Sets Score UI and hides the other UI text until their states turn true
    //maxSpeed set to cap the amount of force applied to player movement so its not infinite
    void Start()
    {
        LoseText.text = "";
        WinText.text = "";
        Collectabletext.text = "";
        ScoreUI();
        maxSpeed = speed + 5f;
    }

    private void Update()
    {
        ScoreUI();
        RestartLevel();
        NextLevel();
    }

    //Since we are dealing with physics, we need to place this function in FixedUpdate
    void FixedUpdate()
    {
        Movement();
    }

    //Sets player movement via physics, player can control the ball until they have successfuly finished the level
    //Player will only be able to have a maximum amount of force applied to them via maxSpeed
    void Movement()
    {
        if(rb.velocity.magnitude >= maxSpeed)
        {
            return;
        }
        if (!gameFinished)
        {
            float xMov = Input.GetAxis("Horizontal");
            float zMov = Input.GetAxis("Vertical");

            moving = new Vector3(xMov, 0f, zMov);

            rb.AddForce(moving * speed);
        }
    }

    //Counts how many collectibles are picked up and updates the score in the Update function
    void ScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
        CollectibleCount();
    }

    //Checks if the player is dead and has pressed "R" to restart the game
    void RestartLevel()
    {
        if (isDead && Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    //Advances you to the next level. Gets the build index number so if youre on the last level, you restart the game on level 1
    void NextLevel()
    {
        if(gameFinished && Input.GetKey(KeyCode.F))
        {
            if(SceneManager.GetActiveScene().buildIndex >= 2)
            {
                SceneManager.LoadScene(0);
            }else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    //Checks how many items were collected
    private void CollectibleCount()
    {
        if (gameFinished)
        {
            switch(score)
            {
                case 1:
                    Collectabletext.text = "You got 1 out of 3 items!\nIs that all you got?";
                    oneCollectable.Play();
                    break;

                case 2:
                    Collectabletext.text = "You got 2 out of 3 items!\nI guess that alright";
                    twoCollectables.Play();
                    break;

                case 3:
                    Collectabletext.text = "You got all 3 items!\nYoure a Pro!";
                    threeCollectables.Play();
                    break;

                default:
                    Collectabletext.text = "You got 0 out of 3 items!\nThe items dont bite I swear";
                    break;
            }
        }
    }

    //Triggers for certain things within the game like picking up objects, dying, and finishing the game
    //Updates the UI respectively to what has been triggered
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            score++;
        }else if(other.gameObject.CompareTag("Death"))
        {
            LoseText.text = "You Lose! Press R to Retry!";
            isDead = true;
        }else if(other.gameObject.CompareTag("Finish"))
        {
            if(SceneManager.GetActiveScene().buildIndex == 2)
            {
                WinText.text = "Game Completed! Press F to restart the game.\nThank you for playing! - Canyon";
                gameFinished = true;
            }else
            {
                WinText.text = "Level Completed! Press F to continue!";
                gameFinished = true;
            }
        }
    }

    //Checks if the player is colliding with the ramp and gives the player a speed boost
    //Resets players speed back to the original state once the player is off the ramp
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ramp"))
        {
            speed = speed * 50;
        }else
        {
            speed = 10;
        }
    }
}
