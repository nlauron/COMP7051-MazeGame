using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {
    public bool music = false;
    public bool dayandnight;
    public AudioClip dayMusic;
    public AudioClip nightMusic;
    private AudioSource bgm;

	// Use this for initialization
	void Start () {
        bgm = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
        dayandnight = Player.day;
        if (Input.GetButtonDown("Music"))
        {
            music = !music;
            
        }

        if (music)
        {
            if (dayandnight)
            {
                bgm.clip = dayMusic;
                bgm.Play();
            }

            if (!dayandnight)
            {
                bgm.clip = nightMusic;
                bgm.Play();
            }
        }
    }
}
