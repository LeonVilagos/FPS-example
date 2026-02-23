using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private float moveVertical, moveHorizontal;
    private float rotateVertical, rotateHorizontal; 
    [SerializeField]
    private float speed = 4.0f, sprintSpeed; 
    private Vector3 movementVector;
    [SerializeField]
    private GameObject CameraObject;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        moveVertical = Input.GetAxis("Vertical");
        moveHorizontal = Input.GetAxis("Horizontal");

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
