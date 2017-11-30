using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    private bool toggle = true;
    public static int winCondition = 0;
    public GameObject pov;
    public Shader mainShader;

    private bool day = true;
    private bool fog = true;
    private bool flashLight = true;

    // Use this for initialization
    void Start() {
        pov.GetComponent<Camera>().SetReplacementShader(mainShader, null);
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
        }
    }

    public void setTime(bool day) {
        this.day = day;
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