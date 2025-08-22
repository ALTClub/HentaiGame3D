using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CameraScript cameraScript;
    [SerializeField]
    float Speed, JumpForce, JumpTime = 0.85f;

    CharacterController charController;
    Animator anim;
    float gravity = 6f, jumpElapsedTime = 0;
    bool isJumping;

    void Start()
    {
        charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        InputManager.Input.Player.Movement.performed += OnMove;
        InputManager.Input.Player.Movement.canceled += OnMove;
        InputManager.Input.Player.Jump.performed += jump;
        InputManager.Input.Player.Jump.canceled += jump;
    }

    private void OnDisable()
    {
        InputManager.Input.Player.Movement.performed -= OnMove;
        InputManager.Input.Player.Movement.canceled -= OnMove;
        InputManager.Input.Player.Jump.performed -= jump;
        InputManager.Input.Player.Jump.canceled -= jump;
    }

    void OnMove(InputAction.CallbackContext ctx)
    {
        anim.SetBool("IsMoving", InputManager.Input.Player.Movement.IsPressed());
    }
    private void FixedUpdate()
    {
        Vector3 direction = InputManager.Input.Player.Movement.ReadValue<Vector2>();
        direction = new Vector3(direction.x, 0, direction.y) * (Speed) * Time.fixedDeltaTime;

        if (isJumping)
        {
            direction.y = Mathf.SmoothStep(JumpForce, JumpForce * 0.30f, jumpElapsedTime / JumpTime) * Time.fixedDeltaTime;
            jumpElapsedTime += Time.fixedDeltaTime;
            if (jumpElapsedTime >= JumpTime)
            {
                isJumping = false;
                jumpElapsedTime = 0;
            }
        }
        direction.y = direction.y - gravity * Time.fixedDeltaTime;

        charController.Move(cameraScript.Rotate(direction, transform));
    }
    void jump(InputAction.CallbackContext ctx)
    {
        if (InputManager.Input.Player.Jump.IsPressed())
        {
            if (charController.isGrounded)
                isJumping = true;
        }
        else if (isJumping) jumpElapsedTime = Mathf.Lerp(jumpElapsedTime, JumpTime, 0.25f);
    }
}
