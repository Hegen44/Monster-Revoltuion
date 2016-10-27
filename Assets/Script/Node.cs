using UnityEngine;
using System.Collections;

public class Node: IHeapItem<Node>{

    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public Node parent;

    // gcost = distance from start
    public int gCost;
    // h cost = heuristic distance
    public int hCost;

    int heapIndex;

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY){
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }
    // calcualte total cost
    public int fCost{
        get{
            return gCost + hCost;
        }
    }
    // index of node inside heap
    public int HeapIndex {
        get {
            return heapIndex;
        }
        set {
            heapIndex = value;
        }
    }
    // compare 2 node
    public int CompareTo(Node nodeToCompare) {
        int compare = fCost.CompareTo(nodeToCompare.fCost); // return 0 or 1
        if(compare == 0) {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare; // return -1
    }
}