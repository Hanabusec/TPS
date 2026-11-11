using UnityEngine;

public class WeaponController : MonoBehaviour
{
    
    private Player player;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player>();
        player.playerControls.Character.Fire.performed += context => Shoot();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Shoot()
     {
       Debug.Log("SHOOT"); 
       anim.SetTrigger("Fire");
       //GetComponentInChildren<Animator>().SetTrigger("Fire");
     }
}
