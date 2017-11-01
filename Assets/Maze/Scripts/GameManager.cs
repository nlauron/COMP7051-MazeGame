using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Maze mazePrefab;

    private Maze mazeInstance;

    // Use this for initialization
    void Start() {
        newGame();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            restart();
        }
    }

    private void newGame() {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        StartCoroutine(mazeInstance.generate());
    }

    private void restart() {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        newGame();
    }
}
