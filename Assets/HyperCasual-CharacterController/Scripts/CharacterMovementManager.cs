using UnityEngine;

public class CharacterMovementManager : MonoBehaviour
{
    [SerializeField] private Canvas inputCanvas;
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator playerAnimator;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;


    private bool isJoystick;

    void Start()
    {
        EnableJoystickInput();
    }

    void Update()
    {
        //Application.targetFrameRate = 60;

        if (isJoystick)
        {
            // Movement
            Vector3 movementDirection = new Vector3(joystick.Direction.x, 0.0f, joystick.Direction.y);
            controller.SimpleMove(movementDirection * movementSpeed);

            if (movementDirection.sqrMagnitude <= 0)
            {
                playerAnimator.SetBool("IsRunning", false);
                return;
            }
            playerAnimator.SetBool("IsRunning", true);

            // Rotation
            Vector3 targetDirection = Vector3.RotateTowards(controller.transform.forward, movementDirection, rotationSpeed * Time.deltaTime, 0.0f);
            controller.transform.rotation = Quaternion.LookRotation(targetDirection);
        }
    }

    public void EnableJoystickInput()
    {
        isJoystick = true;
        inputCanvas.gameObject.SetActive(true);
    }

}
