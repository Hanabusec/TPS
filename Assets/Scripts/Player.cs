using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerControls playerControls;

    public PlayerAim playerAim { get; private set; } //read Only
    public PlayerMovement playerMovement { get; private set; } //read only
    void Awake()

    {
        playerControls = new PlayerControls();
        playerAim = GetComponent<PlayerAim>();
        playerMovement = GetComponent<PlayerMovement>();
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
