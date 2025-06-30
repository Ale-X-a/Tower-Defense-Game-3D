using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshProUGUI))]
public class CoordinateLabeler: MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.paleGreen;
    [SerializeField] private Color blockedColor = Color.brown;

    public Color BlockedColor
    {
        get => blockedColor;
        set => blockedColor = value;
    }
        
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    private Waypoint waypoint;

    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        
        waypoint = GetComponentInParent<Waypoint>();
        DisplayCoordinates();
    }
    
    //TMP - convert, UI
    //TextMeshPro- generic use
    //TMPUGi

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        ColorCoordinates();
            ToggleLabels();
        
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            label.enabled = !label.IsActive();
        }
    }

    void ColorCoordinates()
    {
        { 
            if (waypoint.IsPlaceable)
            {
                label.color = defaultColor;
            }
            else
            {
                label.color = blockedColor;
            }
        }
    }
}