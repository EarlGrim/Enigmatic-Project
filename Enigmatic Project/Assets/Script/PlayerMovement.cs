using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float runSpeed = 7f;
    public float dashSpeed = 10f;
    public float dashTime = 0.2f;
    public Transform cameraTransform;

    private CharacterController controller;
    private bool isDashing = false;
    private float dashTimer = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Handle dashing
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            isDashing = true;
            dashTimer = dashTime;
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
                isDashing = false;
        }

        // Get movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = cameraTransform.forward * moveZ + cameraTransform.right * moveX;
        move.y = 0f;

        // Determine speed based on input
        float currentSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift) && !isDashing)
        {
            currentSpeed = runSpeed; // Running
        }
        if (isDashing)
        {
            currentSpeed = dashSpeed; // Dashing
        }

        controller.Move(move * currentSpeed * Time.deltaTime);
    }
}
