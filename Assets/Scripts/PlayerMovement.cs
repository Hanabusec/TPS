using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     [Header("Controllers")]
     private PlayerControls playerControls;
     private CharacterController characterController;

     [Header("Input Parameter")]
     public Vector2 moveInput;
     public Vector2 aimInput;

     [Header("Movement Parameters")]
     public Vector3 movementDirection;
     [SerializeField] float walkSpeed = 1f;
     [SerializeField] float gravityScale = -9.81f;
     private float verticalVelocity;

     [Header("Aim Parameters")]
     public Vector3 lookDirection;
     [SerializeField] LayerMask aimLayerMask;
     [SerializeField] Transform aimTarget;

     private void Awake()
     {
        playerControls = new PlayerControls();

        //Fire Input Player Action Map
        playerControls.Character.Fire.performed += context => Shoot(); 

        //Movement Input Player Action Map
        playerControls.Character.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        playerControls.Character.Movement.canceled += context => moveInput = Vector2.zero;

        //Aim Input Player Action Map
        playerControls.Character.Aim.performed += context => aimInput = context.ReadValue<Vector2>();
        playerControls.Character.Aim.canceled += context => aimInput = Vector2.zero;    
     }

     private void Start()
     {
        characterController = GetComponent<CharacterController>();
     }

     private void Update()
     {
        ApplyMove();
        AimToMouse();
     }

     private void ApplyMove()
     {
        movementDirection = new Vector3(moveInput.x, 0, moveInput.y);
        ApplyGravity();

        if(movementDirection.magnitude > 0)
        {
          characterController.Move(movementDirection * walkSpeed * Time.deltaTime);
        }
     }

     private void ApplyGravity()
     {
        if(characterController.isGrounded == false)
        {
           verticalVelocity -= gravityScale * Time.deltaTime;
           movementDirection.y = verticalVelocity;
        }
        else
        {
            verticalVelocity = -.5f;
        }
     }

     private void AimToMouse()
     {
        Ray ray = Camera.main.ScreenPointToRay(aimInput);

        if(Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimLayerMask))
        {
            lookDirection = hitInfo.point - transform.position;
            lookDirection.y = 0f;
            lookDirection.Normalize();

            transform.forward = lookDirection;
        }

        aimTarget.position = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);
     }

     private void Shoot()
     {
       Debug.Log("SHOOT"); 
     }


     private void OnEnable()
     {
        playerControls.Enable();
     }
     private void OnDisable()
     {
        playerControls.Disable();
     }



}
