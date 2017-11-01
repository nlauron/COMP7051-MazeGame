using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Maze mazePrefab;
    public GameObject playerPrefab;

    private GameObject playerInstance;

    private Maze mazeInstance;

    // Use this for initialization
    void Start() {
        StartCoroutine(newGame());
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            restart();
        }
    }

    private IEnumerator newGame() {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        yield return StartCoroutine(mazeInstance.generate());
        playerInstance = Instantiate(playerPrefab);
        playerInstance.transform.localPosition = mazeInstance.getCell(mazeInstance.randomCoordinates).transform.localPosition;
    }

    private void restart() {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        if (playerInstance != null) {
            Destroy(playerInstance.gameObject);
        }
        StartCoroutine(newGame());
    }
}
