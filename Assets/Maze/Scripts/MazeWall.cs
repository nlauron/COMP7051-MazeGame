using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWall : MazeCellEdge {
    public Material[] wallMaterial = new Material[4];
    public AudioClip hit;

    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = hit;
    }

    new public void Initialize(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        base.Initialize(cell, otherCell, direction);
        Renderer render = GetComponentInChildren<Renderer>();
        render.material = wallMaterial[(int)direction];
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
        }
    }


}
