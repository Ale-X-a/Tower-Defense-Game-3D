using System.Collections.Generic;
using UnityEngine;

public class GridManager: MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int,Node>(); // Dictionary<Vector2Int,Node> grid = new();
    
    public Dictionary<Vector2Int,Node> Grid { get { return grid; } }
    

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
                //Debug.Log("Coordinates:" + grid [coordinates].coordinates +"isWalkable status:"  + grid [coordinates].isWalkable);
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
    
    public void BlockNode(Vector2Int coordinates)

    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3  position)

    {
        Vector2Int coordinates = new Vector2Int();
        
        coordinates.x = Mathf.RoundToInt(position.x/ UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(position.z / UnityEditor.EditorSnapSettings.move.z);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)

    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * UnityEditor.EditorSnapSettings.move.x;
        position.z = coordinates.y * UnityEditor.EditorSnapSettings.move.z;

        return position;
    }

    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }
}
