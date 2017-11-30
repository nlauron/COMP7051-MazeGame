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
    public AudioClip wall;
    public static int winCondition = 0;
    public static int score;
    public Shader mainShader;
    public static bool day = true;
    public static bool fog = true;
    private bool flashLight = true;
    private float distance;

    // Use this for initialization
    void Start() {
        pov.SetReplacementShader(mainShader, null);
        score = 0;
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = wall;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Phase")) {
            toggle = !toggle;
            gameObject.layer = toggle ? 9 : 10;
        }
        if (Input.GetButtonDown("Fog")) {
            toggleFog();
        }
        if (Input.GetButtonDown("Day")) {
            setTime(!day);
        }
        if (Input.GetButtonDown("Flashlight")) {
            toggleFlashLight();
        }       
        
        if (Input.GetMouseButtonDown(0))
            {
                GameObject ball = Instantiate(ballPrefab, GameObject.Find("HandPosition").transform);
                ball.transform.localPosition = Vector3.zero;
                ball.GetComponent<Rigidbody>().AddForce((pov.transform.forward) * 250);
                ball.transform.parent = GameObject.Find("GameController").transform;
                Destroy(ball, 5.0f);
            }
        scoreHUD.text = "Score: " + score;

        distance = Vector3.Distance(this.transform.position, GameObject.Find("Enemy").transform.position);
        
        if (!fog && distance > 10.0f)
        {
            GetComponentInChildren<AudioSource>().volume = 0.5f;
        }
}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy.loseCondition++;
            Destroy(collision.gameObject);
            SceneManager.LoadScene(0);
        }

        if (collision.gameObject.tag == "Goal")
        {
            winCondition++;
            SceneManager.LoadScene(0);
        }

        if (collision.gameObject.tag == "Wall")
        {
            GetComponent<AudioSource>().Play();
        }
    }

    public void setTime(bool day) {
        Player.day = day;
        Shader.SetGlobalInt("_Day", day ? 1 : 0);
    }

    public void toggleFog() {
        fog = !fog;
        Shader.SetGlobalInt("_Fog", fog ? 1 : 0);
    }

    public void toggleFlashLight() {
        flashLight = !flashLight;
        Shader.SetGlobalInt("_Light", flashLight ? 1 : 0);
    }
}