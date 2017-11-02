using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Maze mazePrefab;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject goalPrefab;

    public Camera mainCamera;

    private GameObject playerInstance;
    private GameObject enemyInstance;
    private GameObject goal;

    private Maze mazeInstance;

    // Use this for initialization
    void Start() {
        StartCoroutine(newGame());
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetButton("Reset"))  {
            restart();
        }
    }

    private IEnumerator newGame() {
        mainCamera.enabled = true;
        mazeInstance = Instantiate(mazePrefab) as Maze;
        yield return StartCoroutine(mazeInstance.generate());
        playerInstance = Instantiate(playerPrefab);
        playerInstance.transform.parent = transform;
        playerInstance.transform.localPosition = mazeInstance.getCell(mazeInstance.randomCoordinates).transform.localPosition;
        enemyInstance = Instantiate(enemyPrefab);
        enemyInstance.transform.parent = transform;
        enemyInstance.transform.localPosition = mazeInstance.getCell(mazeInstance.randomCoordinates).transform.localPosition;
        goal = Instantiate(goalPrefab);
        goal.transform.parent = transform;
        goal.transform.localPosition = mazeInstance.getCell(mazeInstance.randomCoordinates).transform.localPosition;

        mainCamera.enabled = false;
    }

    private void restart() {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        if (playerInstance != null) {
            Destroy(playerInstance.gameObject);
            Destroy(enemyInstance.gameObject);
            Destroy(goal.gameObject);
        }
        StartCoroutine(newGame());
    }
}
