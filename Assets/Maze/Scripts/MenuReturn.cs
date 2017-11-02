using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuReturn : MonoBehaviour {


    public void returnMenu()
    {
        Enemy.loseCondition = 0;
        Player.winCondition = 0;
        Cursor.visible = false;
        SceneManager.LoadScene(2);
    }
}
