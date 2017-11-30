using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWall : MazeCellEdge {
    public Material[] wallMaterial = new Material[4];

    new public void Initialize(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        base.Initialize(cell, otherCell, direction);
        Renderer render = GetComponentInChildren<Renderer>();
        render.material = wallMaterial[(int)direction];
    }


}
