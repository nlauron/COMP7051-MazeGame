using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start : MonoBehaviour { 
    
    public InputField xSize;
    public InputField ySize;
    public static int xMazeSize;
    public static int yMazeSize;
    public GameObject error;

    public void startgame()
    {
        Cursor.visible = true;
        if (int.TryParse(xSize.text, out xMazeSize) && int.TryParse(ySize.text, out yMazeSize))
        {
            Maze.size.x = xMazeSize;
            Maze.size.z = yMazeSize;
            SceneManager.LoadScene(1);
        } else
        {
            error.GetComponent<Text>().enabled = true;
        }
    }
}
