using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class BirdController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    Rigidbody2D body;
    public int score = 0;
    public bool isGameOver = false;
    public GameObject menuScreen;
    public GameObject gameOverScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    void Start()
    {
        
        //PlayerPrefs.SetInt("highScore", 0);
        highScoreText.text = PlayerPrefs.GetInt("highScore").ToString();
        scoreText.text = "";
        body = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
       
        
        if (menuScreen.activeSelf) {
            Time.timeScale = 0;

            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) {
                    menuScreen.SetActive(false);
                    Time.timeScale = 1;

                }
            }
            
        }
        if (gameOverScreen.activeSelf) {

            Time.timeScale = 0;
            
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) {
                    SceneManager.LoadScene(0);

                }
            }
           
        }

        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                Debug.Log("as");
                body.velocity = Vector3.zero;
                body.velocity = Vector3.up * speed  * Time.deltaTime;
               transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 45);
            }

        }
        else {
            if (transform.rotation.z>0) {
                transform.eulerAngles -= new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotationSpeed);
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Pipe" || collision.gameObject.tag == "Base") {
            gameOverScreen.SetActive(true); 
            Debug.Log("Game Over");
        }


    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "CheckScore") {
            score++;

            if (PlayerPrefs.GetInt("highScore") <= score) {
                PlayerPrefs.SetInt("highScore", score);
                scoreText.text = "New High\n" + PlayerPrefs.GetInt("highScore").ToString();
                scoreText.color = Color.green;
            }
            else {
                scoreText.text = score.ToString();
            }

            
        }
    }
}
