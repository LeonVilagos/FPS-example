using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    private float moveVertical, moveHorizontal;
    private float rotateVertical, rotateHorizontal;
    private float sprintSpeed;
    
    private Vector3 movementVector;

    Rigidbody rb;

    [SerializeField]
    private float jumpForce = 5.0f;
    [SerializeField]
    private float speed = 4.0f;
    [SerializeField]
    private GameObject CameraObject;
    [SerializeField]
    InputAction jump;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        jump.Enable();
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void FixedUpdate()
    {
        HandleJumping();
    }

    void HandleMovement()
    {
        moveVertical = Input.GetAxisRaw("Vertical");
        moveHorizontal = Input.GetAxisRaw("Horizontal");

        movementVector = new Vector3(moveHorizontal, 0, moveVertical);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprintSpeed = 2.0f;
        }
        else
        {
            sprintSpeed = 1.0f;
        }

        transform.Translate(movementVector.normalized * speed * sprintSpeed * Time.deltaTime);
    }
    void HandleRotation()
    {
        rotateHorizontal += Input.GetAxis("Mouse X");
        rotateVertical += Input.GetAxis("Mouse Y");

        rotateVertical = Mathf.Clamp(rotateVertical, -90, 90);

        transform.rotation = Quaternion.Euler(0f, rotateHorizontal, 0f);
        CameraObject.transform.rotation = Quaternion.Euler(-1*rotateVertical, rotateHorizontal, 0f);
    }
    void HandleJumping()
    {
        RaycastHit hit;
        if (jump.IsPressed())
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.01f))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
