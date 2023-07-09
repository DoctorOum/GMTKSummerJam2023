using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;

    [SerializeField]
    private GameObject AoeWarningOBJ;
    GameObject currentAOEWarning;

    CharacterController characterController;

    Vector2 turn;

    [SerializeField]
    private float sensitivity = .5f;

    [SerializeField]
    GameObject camObj;

    private void OnEnable()
    {
        instance = this;
    }
    private void OnDestroy()
    {
        Destroy(instance);
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;

        camObj.transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);

        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        { 
            currentAOEWarning = Instantiate(AoeWarningOBJ, new Vector3(transform.position.x + 5, transform.position.y - .5f, transform.position.z), transform.rotation, transform); 
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Destroy(currentAOEWarning, 3);
        }
        
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        characterController.Move(new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal")) * Time.fixedDeltaTime * 2);
    }
}
