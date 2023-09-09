using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 360f;
    private Vector2 inputVector;
    IsometricCharacterRenderer isoRenderer;
    float horizontalInput;
    float verticalInput;
    //float momentum;
    //float velocity = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //inputVector.Set(horizontalInput, verticalInput);

        //inputVector.Normalize();

        rb.MovePosition(transform.position + inputVector.normalized.magnitude * speed * Time.fixedTime * transform.forward);

        // velocity = distance / time
        //rb.AddForce(Vector2.down, ForceMode2D.Impulse);

        //float momentum = rb.mass * velocity;



    }
    /// <summary>
    /// Instead of strafing, turn in the direction the player is looking at.
    /// </summary>
    /// 
    void Look()
    {
        //So it doesn't turn back around when there is no input.
        if (inputVector == Vector2.zero)

        {
            return;
        }
        
       // var rotation = Quaternion.LookRotation(inputVector.ToIso(), Vector2.up);

        //transform.rotation = Quaternion.RotateTowards(transform.rotation,rotation, rotationSpeed);
    }
    void Move()
    {
        //rb.rotation = transform.rotation.
        
        //animator.SetFloat("Speed"
    }
    void FixedUpdate()
    {
        

        Move();

       
    }
}
