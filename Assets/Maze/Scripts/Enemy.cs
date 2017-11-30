using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {
    public int forwardWeight = 2;
    public static int loseCondition = 0;
    public AudioClip hit;

    // Use this for initialization
    void Start ()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = hit;
    }

// Update is called once per frame
    void Update()
    {
        Animator anim = GetComponent<Animator>();
        anim.enabled = true;
        anim.Play("DudeWalk");
        transform.Translate(Vector3.forward * Time.deltaTime);
 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            MazePassage[] passages = findPassages(collision.transform);
            MazePassage randomPassage = passages[Random.Range(0, passages.Length)];

            transform.Rotate(randomPassage.direction.toVector3Rotation() - transform.localRotation.eulerAngles);
        }

        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            loseCondition++;
            SceneManager.LoadScene(0);
        }

        if (collision.gameObject.tag == "Ball")
        {
            Destroy(collision.gameObject);
            Player.score = Player.score + 100;
            GetComponent<AudioSource>().Play();
        }
    }

    private MazePassage[] findPassages(Transform wall)
    {
        List<MazePassage> passages = new List<MazePassage>();
        foreach(Transform tr in wall.parent.transform)
        {
            MazePassage passage = tr.GetComponent<MazePassage>();
            if(passage != null)
            {
                if(passage.direction.toVector3Rotation() == transform.localRotation.eulerAngles)
                {
                    for(int i = 0; i < forwardWeight; i++)
                    {
                        passages.Add(passage);
                    }
                } else
                {
                    passages.Add(passage);

                }
            }
        }
        return passages.ToArray();
    }


}
