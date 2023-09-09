using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class DiveSpit : MonoBehaviour
{
    public Rigidbody projectile;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Capsule));
        //// Instantiate the projectile at the position and rotation of this transform
        //Rigidbody clone;
        //clone = Instantiate(projectile, transform.position, transform.rotation);

        //// Give the cloned object an initial velocity along the current
        //// object's Z axis
        //clone.velocity = transform.TransformDirection(Vector3.forward * 10);
    }
    ///Object Pool script for the enemies
    /*public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    private void Awake()
    {
        SharedInstance = this;

    }
    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }*/

}