using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private bool toggle = true;
    public GameObject ballPrefab;
    public Camera pov;
    public Text scoreHUD;
    public static int winCondition = 0;
    public static int score;

    // Use this for initialization
    void Start() {
        score = 0;
    }

    // Update is called once per frame
    void Update() { 
        if (Input.GetButtonDown("Phase"))
            toggle = !toggle;

        if (toggle)
            gameObject.layer = 9;
        else
            gameObject.layer = 10;

        if (Input.GetMouseButtonDown(0))
        {
            GameObject ball = Instantiate(ballPrefab, GameObject.Find("HandPosition").transform);
            ball.transform.localPosition = Vector3.zero;
            ball.GetComponent<Rigidbody>().AddForce((pov.transform.forward) * 250);
            ball.transform.parent = GameObject.Find("GameController").transform;
            Destroy(ball, 5.0f);
        }

        scoreHUD.text = "Score: " + score;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Enemy.loseCondition++;
            SceneManager.LoadScene(0);
        }

        if (collision.gameObject.tag == "Goal")
        {
            winCondition++;
            SceneManager.LoadScene(0);
            Debug.Log("Hit Hit Hit");
        }
    }
}