using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class PathFinding : MonoBehaviour {

    //public Transform seeker, target;
    PathRequestManager requestManager;

    Grid grid;
    public int adj_cost = 14;  // sqrt(2) * 10
    public int cost = 10; // 1*10

    void Awake(){
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<Grid>();
    }
    /*
    void Update() {
        FindPath(seeker.position, target.position);
    }
    */

    // start find path with coroutine
    public void StartFindingPath(Vector3 startPos, Vector3 targetPos) {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos){

        Stopwatch sw = new Stopwatch();
        sw.Start();

        Vector3[] waypoints = new Vector3[0]; // size 0
        bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        // if start and end vaild
        if(startNode.walkable && targetNode.walkable) {
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0) {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode) {
                    sw.Stop();
                    print("Path Found: " + sw.ElapsedMilliseconds);
                    pathSuccess = true;
                    break;
                }

                foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
                    if (!neighbour.walkable || closedSet.Contains(neighbour)) {
                        continue;
                    }

                    int newCostToNeig = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newCostToNeig < neighbour.gCost || !openSet.Contains(neighbour)) {
                        neighbour.gCost = newCostToNeig;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour)) {
                            openSet.Add(neighbour);
                        }
                        else {
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }
            yield return null; // come back next frame
            if (pathSuccess) {
                waypoints = RetracePath(startNode, targetNode);
            }
            requestManager.FinishProcessingPath(waypoints, pathSuccess);
        }
    }

    Vector3[] RetracePath(Node startNode, Node endNode) {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while(currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    // only place waypoint when change direction
    Vector3[] SimplifyPath(List<Node> path) {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;
        for (int i = 1; i < path.Count; ++i) {
            Vector2 directionNew = new Vector2 (path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionOld != directionNew) {
                waypoints.Add(path[i].worldPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    int GetDistance(Node NodeA, Node nodeB) {
        int disX = Mathf.Abs(NodeA.gridX - nodeB.gridX);
        int disY = Mathf.Abs(NodeA.gridY - nodeB.gridY);

        // adj_cost * disY = cost to get in the same level as target node 
        // cost * (disX - disY) = cost to target node when reach same level
        if (disX > disY)
            return adj_cost * disY + cost * (disX - disY);
        return adj_cost * disX + cost * (disY - disX); ;
    }
}