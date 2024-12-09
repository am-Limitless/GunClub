using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    InputSystem_Actions input;

    Vector2 currentMovement;
    bool movementPressed;
    bool runPressed;

    private void Awake()
    {
        input = new InputSystem_Actions();

        input.Player.Move.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.x != 0 || currentMovement.y != 0;
        };
        input.Player.Sprint.performed += ctx => runPressed = ctx.ReadValueAsButton();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleRotation()
    {
        // Current postion of our player.
        Vector3 currentPosition = transform.position;

        // The change in postion our player should point towards.
        Vector3 newPosition = new Vector3(currentMovement.x, 0, currentMovement.y);

        // Combine the positions to give a postion to look at.
        Vector3 postionToLookAt = currentPosition + newPosition;

        // Rotate the player to face the postionToLookAt.
        transform.LookAt(postionToLookAt);
    }

    private void HandleMovement()
    {
        // Get parameter values from animator.
        bool isRunning = animator.GetBool(isWalkingHash);
        bool isWalking = animator.GetBool(isRunningHash);

        // Start walking if movement pressed is true & not already walking.
        if (movementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }

        // Stop walking if movementPressed is false & already walking.
        if (!movementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        // Start running if movement pressed & run pressed is true and not already running.
        if ((movementPressed && runPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }

        // Stop running if movement pressed or run pressed is false & currently running.
        if ((!movementPressed || !runPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }

    private void OnEnable()
    {
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }
}
