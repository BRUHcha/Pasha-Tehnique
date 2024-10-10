using UnityEngine;


[SelectionBase]
public class FirstPersonController : MonoBehaviour
{
    private Rigidbody rb;

    #region Camera Movement Variables

    public Camera playerCamera;

    [SerializeField] private float fov = 60f;
    [SerializeField] private bool invertCamera = false;
    [SerializeField] private bool cameraCanMove = true;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float maxLookAngle = 50f;

    // Crosshair
    [SerializeField] private bool lockCursor = true;

    // Internal Variables
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    #endregion

    #region Camera Zoom Variables

    [SerializeField] private bool enableZoom = true;
    [SerializeField] private bool holdToZoom = false;
    [SerializeField] private KeyCode zoomKey = KeyCode.Mouse1;
    [SerializeField] private float zoomFOV = 30f;
    [SerializeField] private float zoomStepTime = 5f;

    // Internal Variables
    private bool isZoomed = false;

    #endregion

    #region Movement Variables

    [SerializeField] private bool playerCanMove = true;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float maxVelocityChange = 5f;

    // Internal Variables
    private bool isWalking = false;
    #endregion

    #region Jump

    [SerializeField] private bool enableJump = true;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private float jumpPower = 5f;

    // Internal Variables
    public bool isGrounded = false;

    #endregion

    #region Head Bob

    [SerializeField] private bool enableHeadBob = true;
    [SerializeField] private Transform joint;
    [SerializeField] private float bobSpeed = 10f;
    [SerializeField] private Vector3 bobAmount = new Vector3(.15f, .05f, 0f);

    // Internal Variables
    private Vector3 jointOriginalPos;
    private float timer = 0;

    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if(lockCursor)
            Cursor.lockState = CursorLockMode.Locked;

        // Set internal variables
        playerCamera.fieldOfView = fov;
        jointOriginalPos = joint.localPosition;
    }

    float camRotation;

    private void Update()
    {
        #region Camera

        // Control camera movement
        if(cameraCanMove)
        {
            yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

            if (!invertCamera)
            {
                pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");
            }
            else
            {
                // Inverted Y
                pitch += mouseSensitivity * Input.GetAxis("Mouse Y");
            }

            // Clamp pitch between lookAngle
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            transform.localEulerAngles = new Vector3(0, yaw, 0);
            playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }

        #region Camera Zoom

        if (enableZoom)
        {
            // Changes isZoomed when key is pressed
            // Behavior for toogle zoom
            if(Input.GetKeyDown(zoomKey) && !holdToZoom)
            {
                if (!isZoomed)
                {
                    isZoomed = true;
                }
                else
                {
                    isZoomed = false;
                }
            }

            // Changes isZoomed when key is pressed
            // Behavior for hold to zoom
            if(holdToZoom)
            {
                if(Input.GetKeyDown(zoomKey))
                {
                    isZoomed = true;
                }
                else if(Input.GetKeyUp(zoomKey))
                {
                    isZoomed = false;
                }
            }

            // Lerps camera.fieldOfView to allow for a smooth transistion
            if(isZoomed)
            {
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomFOV, zoomStepTime * Time.deltaTime);
            }
            else if(!isZoomed)
            {
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, fov, zoomStepTime * Time.deltaTime);
            }
        }

        #endregion
        #endregion


        #region Jump

        // Gets input and calls jump method
        if(enableJump && Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        #endregion


        CheckGround();

        if(enableHeadBob)
        {
            HeadBob();
        }
    }

    void FixedUpdate()
    {
        #region Movement

        if (playerCanMove)
        {
            Vector3 targetVelocity;
            // Calculate how fast we should be moving
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            // Checks if player is walking and isGrounded
            // Will allow head bob
            if (input.x != 0 || input.z != 0 && isGrounded)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }

            if(input.magnitude == 0)
                targetVelocity = Vector3.zero;
            else
                targetVelocity = transform.TransformDirection(input.normalized) * walkSpeed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            rb.AddForce(Vector3.ClampMagnitude(velocityChange, maxVelocityChange), ForceMode.VelocityChange);
        }

        #endregion
    }

    // Sets isGrounded based on a raycast sent straigth down from the player object
    private void CheckGround()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
        Vector3 direction = transform.TransformDirection(Vector3.down);
        float distance = 0.8f;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
        {
            Debug.DrawRay(origin, direction * distance, Color.red);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(0f, jumpPower, 0f, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void HeadBob()
    {
        if(isWalking)
        {
            timer += Time.deltaTime * bobSpeed;
            joint.localPosition = new Vector3(jointOriginalPos.x + Mathf.Sin(timer) * bobAmount.x, jointOriginalPos.y + Mathf.Sin(timer) * bobAmount.y, jointOriginalPos.z + Mathf.Sin(timer) * bobAmount.z);
        }
        else
        {
            timer = 0;
            joint.localPosition = new Vector3(Mathf.Lerp(joint.localPosition.x, jointOriginalPos.x, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.y, jointOriginalPos.y, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.z, jointOriginalPos.z, Time.deltaTime * bobSpeed));
        }
    }
}