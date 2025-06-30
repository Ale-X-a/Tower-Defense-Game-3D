using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class EnemyController: MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();

    [SerializeField] [Range(1f, 5f)] private float speed = 1f;

    Enemy enemy;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    void OnEnable()
    {  
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Debug.Log(waypoint.name);
            Vector3 startPosition = transform.position; // transform.position = waypoint.transform.position;
            Vector3 endPosition = waypoint.transform.position;  // yield return new WaitForSeconds(waitTime);
            float travelPercent = 0f;
            
            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed; //travelPercent = travelPercent + (Time.deltaTime * speed);
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
                
            }
        }
        FinishPath();
    }

    void FindPath()
    {
        path.Clear();
        
        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        foreach (Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();
            if (waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }
    
    void ReturnToStart()
    {
        
        transform.position = path[0].transform.position;
    }

    void FinishPath()
    {
        enemy.PenalizeGold();
        gameObject.SetActive(false);
    }
    
}