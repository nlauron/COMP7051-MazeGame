using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    private bool isAnimating = false;

    // Use this for initialization
    void Start () {
		
	}

// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            isAnimating = !isAnimating;
        Animator anim = GetComponent<Animator>();
        if (isAnimating)
        {
            anim.enabled = true;
            anim.Play("DudeWalk");
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        else
        {
            anim.enabled = false;
            anim.StopPlayback();
        }        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
            transform.Rotate(new Vector3 (0,90,0));

        if (collision.gameObject.tag == "Player")
            Destroy(collision.gameObject);
    }

}
