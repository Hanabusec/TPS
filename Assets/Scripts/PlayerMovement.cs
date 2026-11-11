using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     [Header("Controllers")]
     private PlayerControls playerControls;
     private CharacterController characterController;
     private Player player;
     //Animator component erişimini sağlayacak değişkenin tanımlanması
     private Animator animator;

     [Header("Input Parameter")]
     public Vector2 moveInput;
     public Vector2 aimInput;

     [Header("Movement Parameters")]
     public Vector3 movementDirection;
     private float speed;
     [SerializeField] float walkSpeed = 1f;
     [SerializeField] float runSpeed = 4f;
     [SerializeField] float gravityScale = -9.81f;
     private float verticalVelocity;
     private bool isRunning;

     [Header("Aim Parameters")]
     public Vector3 lookDirection;
     [SerializeField] LayerMask aimLayerMask;
     [SerializeField] Transform aimTarget;


     private void Start()
     {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<Player>();

        speed = walkSpeed;

        AssignInputEvents();
     }

     private void Update()
     {
        ApplyMove();
        AimToMouse();
        AnimatorControllers();
     }

     private void AnimatorControllers()
     {
         float xFactor = Vector3.Dot(movementDirection.normalized, transform.right);
         Debug.Log(xFactor);
         float zFactor = Vector3.Dot(movementDirection.normalized,transform.forward);
         Debug.Log(zFactor);

         animator.SetFloat("xFactor",xFactor,.1f, Time.deltaTime);
         animator.SetFloat("zFactor",zFactor,.1f, Time.deltaTime);

         bool playRunAnimation = isRunning && moveInput.magnitude>0;
         
         animator.SetBool("isRunning",playRunAnimation);
     }

     private void ApplyMove()
     {
        movementDirection = new Vector3(moveInput.x, 0, moveInput.y);
        ApplyGravity();

        if(movementDirection.magnitude > 0)
        {
          characterController.Move(movementDirection * speed * Time.deltaTime);
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

        aimTarget.position = new Vector3(hitInfo.point.x, transform.position.y +.6f, hitInfo.point.z);
     }

     //private void Shoot()
     //{
       //Debug.Log("SHOOT"); 
       //animator.SetTrigger("Fire");
     //}

     private void AssignInputEvents()
     {
        //playerControls = new PlayerControls();
        playerControls = player.playerControls;

        //Fire Input Player Action Map
        //playerControls.Character.Fire.performed += context => Shoot(); 

        //Movement Input Player Action Map
        playerControls.Character.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        playerControls.Character.Movement.canceled += context => moveInput = Vector2.zero;

        //Aim Input Player Action Map
        playerControls.Character.Aim.performed += context => aimInput = context.ReadValue<Vector2>();
        playerControls.Character.Aim.canceled += context => aimInput = Vector2.zero;    

        //Run Input Player Action Map
        playerControls.Character.Run.performed += context => 
        {
            speed = runSpeed;
            isRunning = true;
        };
        
        playerControls.Character.Run.canceled += context => 
        {
            speed = walkSpeed;
            isRunning = false;
        };

     }

}
