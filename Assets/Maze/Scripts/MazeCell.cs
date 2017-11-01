using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour {
    public IntVector2 coordinates;

    private int initializedEdgeCount;
    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];

    public bool isFullyInitialized {
        get {
            return initializedEdgeCount == MazeDirections.Count;
        }
    }

    public MazeCellEdge getEdge(MazeDirection direction) {
        return edges[(int)direction];
    }

    public void setEdge (MazeDirection direction, MazeCellEdge edge) {
        edges[(int)direction] = edge;
        initializedEdgeCount += 1;
    }

    public MazeDirection randomUninitializedDirection {
        get {
            int skips = Random.Range(0, MazeDirections.Count - initializedEdgeCount);
            for (int i = 0; i < MazeDirections.Count; i++) {
                if (edges[i] == null) {
                    if (skips == 0) {
                        return (MazeDirection)i;
                    }
                    skips -= 1;
                }
            }
            throw new System.InvalidOperationException("MazeCell has no unitialized directions");
        }
    }
}
