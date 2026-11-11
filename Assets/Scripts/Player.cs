using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerControls playerControls;
    
    void Awake()

    {
        playerControls = new PlayerControls();
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
