using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
 [Range(0f, 2f)]
    [SerializeField] private float waypointSize = 1f;
    private void OnDrawGizmos()
    {
        foreach(Transform t in transform)
        {
            //Show all the points in the editor view
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(t.position, waypointSize);
        }
        Gizmos.color = Color.red;

        for(int i = 0; i < transform.childCount-1; i++)
        {
            //Get all the points and draw lines between them in the editor view
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i+1).position);
        }
        Gizmos.DrawLine(transform.GetChild(transform.childCount-1).position, transform.GetChild(0).position);
    }

    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
        if(currentWaypoint == null)
        {
            //If there is not a valid point, default to the starting waypoint
            return transform.GetChild(0);
        }

        if(currentWaypoint.GetSiblingIndex() < transform.childCount -1)
        {
            //If there are still waypoints in the object, get the next one in series through the siblings
            return transform.GetChild(currentWaypoint.GetSiblingIndex()+1);  
        }
        else
        {
            //If all else fails, default to the first waypoint in the index
            return transform.GetChild(0);
        }
    }

    public Transform GetPreviousWaypoint(Transform currentWaypoint)
    {
        if(currentWaypoint == null)
        {
            //If there is not a valid point, default to the last waypoint
            return transform.GetChild(transform.childCount-1);
        }
        if(currentWaypoint.GetSiblingIndex() > 0)
        {
            //If there are still waypoints in the object, get the next one in series through the siblings
            return transform.GetChild(currentWaypoint.GetSiblingIndex()-1);  
        }
        else
        {
            return transform.GetChild(0);
        }
    }
}
