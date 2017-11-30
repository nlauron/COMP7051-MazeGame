using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour {

    public Text title;

	// Use this for initialization
	void Start () {
        Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Player.loseCondition >= 1)
        {
            title.color = Color.red;
            title.text = "You Lose";
        }else if (Player.winCondition >=1)
        {
            title.color = Color.green;
            title.text = "You Win";
        }
    }
}
