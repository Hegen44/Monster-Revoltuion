using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

    public bool displayGridGizmos;

    //public Transform target;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public LayerMask unwalkableMask;

    Node[,] grid;
    Vector3 gridWorldPos;
    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Awake()
    {
        gridWorldPos = GetComponent<Transform>().position;
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    public int MaxSize {
        get {
            return gridSizeX * gridSizeY;
        }
    }

    // Create Grid
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2; 
        for (int x = 0; x< gridSizeX; x++)
        { 
            for(int y = 0; y < gridSizeY; y ++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapArea(worldPoint, worldPoint + new Vector3(nodeRadius, nodeRadius,0), unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node) {
        List<Node> nighbours = new List<Node>();

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <=1; y++) {
                if (x == 0 && y == 0)
                    continue;
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if(checkX >= 0 && checkX < gridSizeX && checkX >= 0 && checkY < gridSizeY) {
                    nighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return nighbours;
    }

    public bool CheckDiagonalWalkable(Node currentNode, Node neighbourNode)
    {
        Vector2 movement_vector = new Vector2(neighbourNode.worldPosition.x - currentNode.worldPosition.x, neighbourNode.worldPosition.y - currentNode.worldPosition.y);
        movement_vector.Normalize();
        if (movement_vector.x != 0 && movement_vector.y != 0)
        {
            if (!grid[currentNode.gridX, neighbourNode.gridY].walkable || !grid[neighbourNode.gridX, currentNode.gridY].walkable)
            {
                return false;
            }
        }
        return true;
    }

    // Calculate target position in the world
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x - gridWorldPos.x + gridWorldSize.x/2) / gridWorldSize.x;
        float percentY = (worldPosition.y - gridWorldPos.y + gridWorldSize.y/2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    // draw grid, should be disable in game
	void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position,new Vector3(gridWorldSize.x, gridWorldSize.y,1));
        //Node targetNode = NodeFromWorldPoint(target.position);
        if (grid != null && displayGridGizmos) {
            foreach (Node n in grid) {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                //if (targetNode == n) Gizmos.color = Color.blue;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
