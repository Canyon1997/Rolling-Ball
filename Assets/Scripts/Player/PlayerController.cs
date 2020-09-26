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

    public Text scoreText;
    public Text LoseText;
    public Text WinText;
    public Text Collectabletext;

    public float speed = 10f;

    private float maxSpeed;
    private int score = 0;

    bool gameFinished;
    bool isDead;

    private void Awake() => rb = GetComponent<Rigidbody>();

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

    void FixedUpdate()
    {
        Movement();
    }

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

    void ScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
        CollectibleCount();
    }

    void RestartLevel()
    {
        if (isDead && Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void NextLevel()
    {
        if(gameFinished && Input.GetKey(KeyCode.F))
        {
            //Checks if youre on the last level in the array, loads the first level if true.
            if(SceneManager.GetActiveScene().buildIndex >= 2)
            {
                SceneManager.LoadScene(0);
            }else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void CollectibleCount() //Checks how many items were collected
    {
        if (gameFinished)
        {
            switch(score)
            {
                case 1:
                    Collectabletext.text = "You got 1 out of 3 items!\nIs that all you got?";
                    break;

                case 2:
                    Collectabletext.text = "You got 2 out of 3 items!\nI guess that alright";
                    break;

                case 3:
                    Collectabletext.text = "You got all 3 items!\nYoure a Pro!";
                    break;

                default:
                    Collectabletext.text = "You got 0 out of 3 items!\nThe items dont bite I swear";
                    break;
            }
        }
    }

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
            WinText.text = "Level Completed! Press F to continue!";
            gameFinished = true;
        }
    }

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
