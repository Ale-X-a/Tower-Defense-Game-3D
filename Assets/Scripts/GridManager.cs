using System.Collections.Generic;
using UnityEngine;

public class GridManager: MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [SerializeField] Node mode;
    
    Dictionary<Vector2Int,Node> grid = new Dictionary<Vector2Int,Node>(); // Dictionary<Vector2Int,Node> grid = new();
    
    public Dictionary<Vector2Int,Node> Grid { get { return grid; } }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        CreateGrid();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
                Debug.Log("Coordinates:" + grid [coordinates].coordinates +"isWalkable status:"  + grid [coordinates].isWalkable);
            }
        }
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid [coordinates];
        }
        return null;
    }
}
