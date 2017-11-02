using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour {

    public void playAgain()
    {
        Enemy.loseCondition = 0;
        Player.winCondition = 0;
        Cursor.visible = false;
        SceneManager.LoadScene(1);
    }
}
