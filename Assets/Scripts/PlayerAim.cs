using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Player player;

    private PlayerControls playerControls;

    private Vector2 aimInput;

    [Header("Aim Parameters")]
    public Vector3 lookDirection;
    [SerializeField] LayerMask aimLayerMask;
    [SerializeField] Transform aimTarget;

    void Start()
    {
        player = GetComponent<Player>();
        AssignInputEvents();
    }

    // Update is called once per frame
    void Update()
    {
        aimTarget.position = GetMousePosition();
        aimTarget.position = new Vector3(aimTarget.position.x, transform.position.y + .6f, aimTarget.position.z);
    }

    public Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(aimInput);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimLayerMask))
        {
            return hitInfo.point;
        }

        return Vector3.zero;
    }

    private void AssignInputEvents()
    {
        playerControls = player.playerControls;

        playerControls.Character.Aim.performed += context => aimInput = context.ReadValue<Vector2>();
        playerControls.Character.Aim.canceled += context => aimInput = Vector2.zero;

    }
}
