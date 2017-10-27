using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToggle : MonoBehaviour { 

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            transform.Rotate(new Vector3(180, 0, 0));
        }

    }
}