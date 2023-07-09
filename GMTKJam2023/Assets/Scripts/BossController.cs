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

    [SerializeField]
    GameObject camFollowObj;

    [SerializeField]
    GameObject bossMesh;

    public List<BossAbilities> abilities = new List<BossAbilities>();

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
        //turn.x += Input.GetAxis("Mouse X") * sensitivity;
        //turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        transform.localRotation = Quaternion.Euler(new Vector3(transform.rotation.x, Camera.main.transform.rotation.y * 100, transform.rotation.z));
        //transform.rotation = Camera.main.transform.rotation;

        //camFollowObj.transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        //camObj.transform.LookAt(camFollowObj.transform);

        //if (Input.GetKeyDown(KeyCode.Mouse0)) 
        //{ 
        //    currentAOEWarning = Instantiate(AoeWarningOBJ, new Vector3(transform.position.x + 6, transform.position.y - .5f, transform.position.z), transform.rotation, transform); 
        //}
        //else if (Input.GetKeyUp(KeyCode.Mouse0))
        //{
        //    Destroy(currentAOEWarning, 3);
        //}

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (abilities[0].currentCooldownTime <= 0)
            {
                abilities[0].UseAbility();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (abilities[1].currentCooldownTime <= 0)
            {
                abilities[1].UseAbility();
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (abilities[2].currentCooldownTime <= 0)
            {
                abilities[2].UseAbility();
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {

            if (abilities[3].currentCooldownTime <= 0)
            {
                abilities[3].UseAbility();
            }
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        moveDirection = Quaternion.AngleAxis(camObj.transform.rotation.eulerAngles.y, Vector3.up) * moveDirection;
        moveDirection.Normalize();

        
        characterController.Move(moveDirection * Time.fixedDeltaTime * 2);
        
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            //"1" is rotation speed
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 1 * Time.fixedDeltaTime);
        }
    }
}
