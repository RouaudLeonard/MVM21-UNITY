using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class DiveSpit : MonoBehaviour
{
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Instantiate<GameObject>;
        //// Instantiate the projectile at the position and rotation of this transform
        //Rigidbody clone;
        //clone = Instantiate(projectile, transform.position, transform.rotation);

        //// Give the cloned object an initial velocity along the current
        //// object's Z axis
        //clone.velocity = transform.TransformDirection(Vector3.forward * 10);
    }
    ///Object Pool script for the enemies

}