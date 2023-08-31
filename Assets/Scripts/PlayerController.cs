using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 360f;
    private Vector3 inputVector;

    float horizontalInput;
    float verticalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Move();
    }
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        PlayerInput();
        Look();
    }

    void PlayerInput()
    {
        inputVector = new(horizontalInput, 0, verticalInput);
    }
    /// <summary>
    /// Instead of strafing, turn in the direction the player is looking at.
    /// </summary>
    /// 
    void Look()
    {
        //So it doesn't turn back around when there is no input.
        if (inputVector == Vector3.zero)

        {
            return;
        }

        var rotation = Quaternion.LookRotation(inputVector.ToIso(), Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
    }
    void Move()
    {
        rb.MovePosition(transform.position + inputVector.normalized.magnitude * speed * Time.deltaTime * transform.forward);
    }
}
