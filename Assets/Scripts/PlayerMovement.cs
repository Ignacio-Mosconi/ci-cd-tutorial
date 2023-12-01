using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform cameraController;
    [SerializeField, Range(0.1f, 10f)] private float moveSpeed = 5f;
    [SerializeField, Range(0.1f, 15f)] private float jumpSpeed = 10f;
    [SerializeField, Range(0.1f, 180f)] private float rotateSpeed = 90f;

    private CharacterController characterController;
    private float currentJumpSpeed;


    void Start ()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update ()
    {
        Vector3 motion = Vector3.zero;
        Vector3 forward;
        float rotationDelta = Mathf.Deg2Rad * rotateSpeed * Time.deltaTime;

        motion += GetMovementMotion(out forward);
        motion += GetJumpMotion();

        characterController.Move(motion);
        transform.forward = Vector3.RotateTowards(transform.forward, forward, rotationDelta, 0f);
    }


    private Vector3 GetMovementMotion (out Vector3 forward)
    {
        Vector3 motion;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float motionDelta = moveSpeed * Time.deltaTime;
        
        Vector3 moveInput = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        
        forward = new Vector3(cameraController.forward.x, 0f, cameraController.forward.z).normalized;
        Vector3 right = new Vector3(cameraController.right.x, 0f, cameraController.right.z).normalized;

        motion = (forward * moveInput.z + right * moveInput.x) * motionDelta;

        return motion;
    }

    private Vector3 GetJumpMotion ()
    {
        Vector3 motion;

        bool hasJustJumped = false;

        if (Input.GetButtonDown("Jump") && currentJumpSpeed <= 0f)
        {
            currentJumpSpeed = jumpSpeed;
            hasJustJumped = true;
        }

        motion = new Vector3(0f, currentJumpSpeed * Time.deltaTime, 0f);

        currentJumpSpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded && !hasJustJumped)
            currentJumpSpeed = 0f;

        return motion;
    }
}