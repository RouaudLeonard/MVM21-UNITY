using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    
    [Header("References")]
   // private PlayerMovement pm;  Whatever the player movement script is
    public Transform cm; // camera variable
    public Transform tonguePoint; //where the player is looking
    public Transform mouthTip; // point where the raycast is launched
    public LayerMask whatIsHarpoonable;
    public LineRenderer lr;
    public Rigidbody rb;
    //public Transform debugTransform;

    // Vector3 mouseWorldPosition = Vector3.zero;

    [Header("Harpooning")]
    public float maxHarpoonDistance;
    public float grappleDelayTime;
    public float grapple_speed;
    //public float base_grapple_speed_up; //impulse
    //private float grapple_speed_up_add = 2; //is caluated in the Calculate_jump method
    //public int lob; // divid disance by this to get the grpple_spped_up_add
    private float distance = 10; //distance betwean this and the intended location
    /// It wasn't reading the hit.point (It always caught it as the origin point 0,0,0)   public Vector3 GrappleVec; //saved vector for the targeted grapple location
    public float distance_reset = 2; //when objects get distance_reset close to target location,
                                     //private bool grappling = false;
                                     //  public LayerMask available_grapple_area; //Make sure this is set to whatIsGround, since the ground is assumed to be grapplable
                                     // Vertex point is Hit Vertex



    private Vector3 grapplePoint;
    public float overshootYAxis;




    [Header("Cooldown")]
    public float grapplingCd;
    private float grapplingCdTimer;

    //[Header("Input")]//Won't need the input this way because of new input System
    //public KeyCode grappleKey = KeyCode.Mouse1;

    private bool grappling;



    private void Start()
    {
        //GrappleVec = new Vector3(20, 4, 9);//just a text
        rb = GetComponent<Rigidbody>();
       // pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {



        /*if (Input.GetKeyDown(grappleKey)) StartGrapple(); //Don't need this input because of Player Input
        */

        /*if(Input.GetKeyDown(KeyCode.U))//this also part of the grapple test
            {
            ExcuteGrapple();
            grappling = true;
        }*/


        if (grapplingCdTimer > 0)
        {
            grapplingCdTimer -= Time.deltaTime;
        }

        if (grappling == true)
        {


        }




    }
    private void LateUpdate()
    {
        if (grappling)
            lr.SetPosition(0, mouthTip.position);

    }



    private void StartGrapple()
    {

        if (grapplingCdTimer > 0) return;




        grappling = true;
       // pm.freeze = true;

        RaycastHit hit;
        if (Physics.Raycast(cm.position, tonguePoint.forward, out hit, maxHarpoonDistance, whatIsHarpoonable))
        {

            //Hit something to store the hit point
            grapplePoint = hit.point;

            //call Execute Grapple with a delay
            Invoke(nameof(ExcuteGrapple), grappleDelayTime);




            if (hit.transform.gameObject.layer == whatIsHarpoonable)
            {

                ExcuteGrapple();
                grappling = true;

                //this is the toggle for invoke it
                Debug.Log("Hit grapple thing");
            }



        } //camera position and  .forward
        else
        {
            grapplePoint = tonguePoint.position + tonguePoint.forward * maxHarpoonDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);  //not needed
        }
        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
    }

    private void ExcuteGrapple()
    {

    }
    public bool IsGrappling()
    {
        return grappling;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
    public void StopGrapple()
    {
//        pm.freeze = false;

        grappling = false;

        grapplingCdTimer = grapplingCd;

        lr.enabled = false;
    }


    void OnGrapple()
    {
        Debug.Log("I'm grappling thing");
        StartGrapple();

    }
}
