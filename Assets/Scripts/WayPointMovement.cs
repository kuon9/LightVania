using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMovement : MonoBehaviour
{
    [SerializeField] Transform [] waypoints;
    [SerializeField] int waypointIndex = 0; //
    [SerializeField] float speed = 5f;

    public bool isMoving;
       void Start()
    {
        isMoving = false;
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving) {return ;} // if platform isn't moving then return and don't proc move ();
        Move();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position,waypoints[waypointIndex].transform.position, speed* Time.deltaTime);
        if(transform.position == waypoints[waypointIndex].transform.position) // 
        {
            waypointIndex +=1;
        }
        if(waypointIndex == waypoints.Length) // when gameObject reaches last waypoint then it resets back to the first waypoint.
        {
            waypointIndex = 0;    
        }       
        //waypointIndex = 0;
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider == true)
        {
            isMoving = true; // Platform is only active after colliding with player.
            print("Platform proc");
        }
    }
}
