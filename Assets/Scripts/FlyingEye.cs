using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    [SerializeField] Transform [] waypoints;
    [SerializeField] int waypointIndex = 0; //
    [SerializeField] float speed = 5f;
    [SerializeField] float Timer = 5f;
    bool facingRight = true;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if(Timer  <= 0f)
        {
            StartCoroutine(Flip());
            ResetTimer();
        }
        Flight();
    }

    void Flight()
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
    public IEnumerator Flip()
    {
        transform.localScale = new Vector3(-2,2,2);
        yield return new WaitForSeconds(3);
        transform.localScale = new Vector3(2,2,2);    
    }

    void ResetTimer()
    {
        Timer = 5f;    
    }

}
