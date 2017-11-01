using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private bool toggle;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() { 
        if (Input.GetButton("Reset"))
            Application.LoadLevel(Application.loadedLevel);

        if (Input.GetButton("Phase"))
            toggle = !toggle;

        if (toggle)
            gameObject.layer = 9;
        else
            gameObject.layer = 10;

    }
}