using UnityEngine;

public class Waypoint: MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    [SerializeField] Tower towerPrefab;

    public bool IsPlaceable
    {
        get { return isPlaceable; }
    }
    

    void OnMouseDown()
    {
        if (isPlaceable)
        {
            //Debug.Log("Placing:" + transform.name);
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            isPlaceable = !isPlaced;

        }
    }
}
