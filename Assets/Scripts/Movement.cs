using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    private float moveVertical, moveHorizontal;
    private float rotateVertical, rotateHorizontal; 
    [SerializeField]
    private float speed = 4.0f, sprintSpeed; 
    private Vector3 movementVector;
    [SerializeField]
    private GameObject CameraObject;
    [SerializeField]
    InputAction jump;
    [SerializeField]
    float jumpForce = 5.0f;

    Rigidbody rb;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (jump.IsPressed())
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.01f))
            {
                rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void OnEnable()
    {
        jump.Enable();
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

        //Debug.Log(rotateVertical + " " + rotateHorizontal);
        rotateVertical = Mathf.Clamp(rotateVertical, -90, 90);

        transform.rotation = Quaternion.Euler(0f, rotateHorizontal, 0f);
        CameraObject.transform.rotation = Quaternion.Euler(-1*rotateVertical, rotateHorizontal, 0f);
        
    }
}
