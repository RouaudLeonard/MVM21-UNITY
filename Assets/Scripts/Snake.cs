using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Snake : MonoBehaviour
{
    //[SerializeField]
    //Vector2 snakePos;

   // public Camera cam;
   [SerializeField]
    private GameObject waterBall;
    
    private void Start()
    {
        //waterBall = GetComponent<GameObject>();
       // wayPoint = GameObject.Find("wayPoint");
    }

    // Update is called once per frame
    void Update()
    {
        waterBall = PObjectPool.SharedInstance.GetPooledObject();


        waterBall.transform.position = transform.position;
        waterBall.SetActive(true);


        Attack();

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
           // Ray2D ray = cam.ScreenPointToRay(Input.mousePosition);
                
               // cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit;

          //  ray

            //if (Physics2D.Raycast(ray, out hit))
            //{
            //    //Move the agent
            //    agent.SetDestination(hit.point);
            //}


        }

       // wayPointPos = new Vector3(wayPoint.transform.position.x, transform.position.y, wayPoint.transform.position.z);
        //Here, the zombie's will follow the waypoint.
       // transform.position = Vector3.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
    }

    void Attack()
    {
        Debug.Log("Attack Started");


    

      
    }
    /*Start()
{//initialize variables
}

Update()
{
// using navmesh to move around an area in the dive state(ai navigation)
}

Fixed Update()
{ //Check surroundings for player

if find player
(Attack();)

//if/else for the scan

// no player, does normal snake thing
}

Attack()
{
// animation.Play()(surfacing)
// if really close()
{animation.Play()(Attacking)
//instantiate object(object  will be following the player and does damage on contact)
}

Hooked()
{// Animation.Play()hooked
// enemy health bar -= over time by an interval
//stops moving(if maximum health bar at a certain level, they keep moving)
}

Dead()
{// “destroyed”(disabled) when health bar is gone
//Animation.Play()vfx if have it or dead animation
}



Spit Attack()
{//get its current position
// get player’s position
// subtract/ add the difference in position over time to be equal

// when equal deal damage.
}
*/

    //You may consider adding a rigid body to the zombie for accurate physics simulation
    private GameObject wayPoint;

    private Vector2 wayPointPos;

    //This will be the zombie's speed. Adjust as necessary.
    private float speed = 6.0f;

}
