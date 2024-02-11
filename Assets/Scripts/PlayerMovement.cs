using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float Speed, JumpForce;
    [SerializeField][Range(1,10)] private float CancelJumpForce;
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform model;
    private Rigidbody rb;
    private Animator anim;
    [SerializeField] CameraScript cam;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        InputManager.Input.Player.Movement.performed += ctx => OnMove();
        InputManager.Input.Player.Movement.canceled += ctx => OnMove();
        InputManager.Input.Player.Jump.performed += ctx => jump();
        InputManager.Input.Player.Jump.canceled += ctx => cancelJump();
    }
    void OnMove()
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
        model.localRotation = Quaternion.Slerp(model.localRotation,Quaternion.LookRotation(direction), rotationSpeed * Time.fixedDeltaTime); 
        //поворачиваем модельку в сторону движения
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground")) anim.SetBool("IsGrounded", true);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground")) anim.SetBool("IsGrounded", false);
    }
    void jump()
    {
        if (anim.GetBool("IsGrounded")) rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }
    void cancelJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }
}
