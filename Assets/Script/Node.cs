using UnityEngine;
using System.Collections;

public class Node{

    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public Node parent;

    // gcost = distance from start
    public int gCost;
    // h cost = heuristic distance
    public int hCost;

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY){
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost{
        get{
            return gCost + hCost;
        }
    }
}