using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {
    public static IntVector2 size;
    public MazeCell cellPrefab;
    public float generationStepDelay;
    public MazePassage passagePrefab;
    public MazeWall wallPrefab;

    private MazeCell[,] cells;

    public MazeCell getCell(IntVector2 coords) {
        return cells[coords.x, coords.z];
    }

    public MazeCell getCellFromCoords(Vector3 coord) {
        int closestX = Mathf.RoundToInt(coord.x + size.x * 0.5f - 0.5f);
        int closestZ = Mathf.RoundToInt(coord.z + size.z * 0.5f - 0.5f);
        return cells[closestX, closestZ];
    }

    public IEnumerator generate() {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0) {
            yield return delay;
            DoNextGenerationStep(activeCells);
        }
    }

    private MazeCell createCell(IntVector2 coords) {
        MazeCell cell = Instantiate(cellPrefab) as MazeCell;
        cells[coords.x, coords.z] = cell;
        cell.coordinates = coords;
        cell.name = "Maze Cell " + coords.x + ", " + coords.z;
        cell.transform.parent = transform;
        cell.transform.localPosition = new Vector3(coords.x - size.x * 0.5f + 0.5f, 0f, coords.z - size.z * 0.5f + 0.5f);
        return cell;
    }

    public IntVector2 randomCoordinates {
        get {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
        }
    }

    private void DoFirstGenerationStep(List<MazeCell> activeCells) {
        activeCells.Add(createCell(randomCoordinates));
    }

    private void DoNextGenerationStep(List<MazeCell> activeCells) {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        if (currentCell.isFullyInitialized) {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        MazeDirection direction = currentCell.randomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.toIntVector2();
        if (containsCoordinates(coordinates)) {
            MazeCell neighbour = getCell(coordinates);
            if(neighbour == null) {
                neighbour = createCell(coordinates);
                createPassage(currentCell, neighbour, direction);
                activeCells.Add(neighbour);
            } else {
                createWall(currentCell, neighbour, direction);
            }
        } else {
            createWall(currentCell, null, direction);
        }
    }

    private void createPassage (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.getOpposite());
    }

    private void createWall (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazeWall wall = Instantiate(wallPrefab) as MazeWall;
        wall.Initialize(cell, otherCell, direction);
        if(otherCell != null) {
            wall = Instantiate(wallPrefab) as MazeWall;
            wall.Initialize(otherCell, cell, direction.getOpposite());
        }
    }

    public bool containsCoordinates (IntVector2 coordinate) {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }
}
