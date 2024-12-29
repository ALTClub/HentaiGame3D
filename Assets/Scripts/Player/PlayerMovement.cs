using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform model;
    [SerializeField] CameraScript cam;

    [SerializeField] float rotationSpeed;

    [SerializeField]
    private float Speed, JumpForce;

    [Range(1, 10)]
    [SerializeField]
    private float CancelJumpForce;

    private Rigidbody rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        InputManager.Input.Player.Movement.performed += OnMove;
        InputManager.Input.Player.Movement.canceled += OnMove;
        InputManager.Input.Player.Jump.performed += jump;
        InputManager.Input.Player.Jump.canceled += cancelJump;
    }

    private void OnDisable()
    {
        InputManager.Input.Player.Movement.performed -= OnMove;
        InputManager.Input.Player.Movement.canceled -= OnMove;
        InputManager.Input.Player.Jump.performed -= jump;
        InputManager.Input.Player.Jump.canceled -= cancelJump;
    }

    void OnMove(InputAction.CallbackContext ctx)
    {
        anim.SetBool("IsMoving", InputManager.Input.Player.Movement.IsPressed());
    }

    private void FixedUpdate()
    {
        Vector3 direction = InputManager.Input.Player.Movement.ReadValue<Vector2>();
        direction = new Vector3(direction.x, 0, direction.y);

        rb.AddRelativeForce(direction * Speed * Time.fixedDeltaTime, ForceMode.Impulse);

        if (!InputManager.Input.Player.Movement.IsPressed()) return;

        cam.Rotate(rotationSpeed, transform); //поворачиваем игрока в центр камеры

        //поворачиваем модельку в сторону движения
        model.localRotation = Quaternion.Slerp(model.localRotation, Quaternion.LookRotation(direction), rotationSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            anim.SetBool("IsGrounded", true);
        }
    }

    private void OnCollisionExit(UnityEngine.Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            anim.SetBool("IsGrounded", false);
        }
    }

    void jump(InputAction.CallbackContext ctx)
    {
        if (anim.GetBool("IsGrounded"))
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    void cancelJump(InputAction.CallbackContext ctx)
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
    }
}
