using System.Collections.Generic;
using UnityEngine;

public class PathFinder: MonoBehaviour
{
    [SerializeField] Vector2Int  startCoordinates;
    [SerializeField] Vector2Int  endCoordinates;
    
    Node startNode;
    Node endNode;
    Node currentSearchNode;
    
    Queue<Node>  frontier = new Queue<Node>();
    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};

    GridManager gridManager;
 
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>(); 
 
 // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        gridManager = FindFirstObjectByType<GridManager>();

        if (gridManager != null)
        {
            grid = gridManager.Grid;
        }
    }
    void Start()
    {
        startNode = new Node(startCoordinates, true);
        endNode = new Node(endCoordinates, true);
        
        BreadthFirstSearch();
        BuildPath();
        
    }
    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;

            if (grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
                
            }
        }
        

        foreach (Node neighbor in neighbors)
        {
            if (reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadthFirstSearch()
    {
        bool isRunning = true; //the Search is running
        
        frontier.Enqueue(startNode);
        frontier.Enqueue(endNode);
        
        reached.Add(startCoordinates, startNode);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            
            ExploreNeighbors();

            if (currentSearchNode.coordinates == endCoordinates)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        
        Node currentNode = endNode;
        
        path.Add(currentNode);
        
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }
        path.Reverse();
        
        return path;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
