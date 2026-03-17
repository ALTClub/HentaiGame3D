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
        anim.SetBool("IsMoving", !ctx.canceled);
    }
    private void FixedUpdate()
    {
        if (anim.GetBool("IsSitting")) return;
        Vector3 direction = InputManager.Input.Player.Movement.ReadValue<Vector2>();
        direction = new Vector3(direction.x, 0, direction.y) * (Speed) * Time.fixedDeltaTime;

        if (anim.GetBool("IsJumping"))
        {
            direction.y = Mathf.SmoothStep(JumpForce, JumpForce * 0.30f, jumpElapsedTime / JumpTime) * Time.fixedDeltaTime;
            jumpElapsedTime += Time.fixedDeltaTime;
            if (jumpElapsedTime >= JumpTime)
            {
                anim.SetBool("IsJumping", false);
                jumpElapsedTime = 0;
            }
        }
        direction.y = direction.y - gravity * Time.fixedDeltaTime;

        charController.Move(cameraScript.Rotate(direction, transform));
    }
    void jump(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled)
        {
            if (charController.isGrounded)
                anim.SetBool("IsJumping", true);
        }
        else if (anim.GetBool("IsJumping")) jumpElapsedTime = Mathf.Lerp(jumpElapsedTime, JumpTime, 0.25f);
    }
}
