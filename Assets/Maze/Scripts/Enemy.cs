using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    private bool isAnimating = false;
    private GameObject wall;

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
            transform.Translate(Vector3.forward * Time.deltaTime * 2);
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
        {
            int turn = Random.Range(1, 4);
            if (turn == 1)
                transform.Rotate(new Vector3(0, 90, 0));
            else if (turn == 2)
                transform.Rotate(new Vector3(0, -90, 0));
            else if (turn == 3)
                transform.Rotate(new Vector3(0, 180, 0));
        }

            if (collision.gameObject.tag == "Player")
            Destroy(collision.gameObject);
    }

}
