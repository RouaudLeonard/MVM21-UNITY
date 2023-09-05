using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 360f;
    private Vector2 inputVector;
    IsometricCharacterRenderer isoRenderer;
    float horizontalInput;
    float verticalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        inputVector = new(horizontalInput, verticalInput);

        inputVector = Vector2.ClampMagnitude(inputVector, 1);

        Move();
       // isoRenderer.SetDirection(inputVector);
    }
    void Update()
    {

        Look();
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

        var rotation = Quaternion.LookRotation(inputVector.ToIso(), Vector2.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
    }
    void Move()
    {
        rb.MovePosition(transform.position + inputVector.normalized.magnitude * speed * Time.deltaTime * transform.forward);
    }
}
