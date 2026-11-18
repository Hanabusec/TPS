using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private WeaponChange weaponchange;
    
    void Start()
    {
        weaponchange = GetComponentInParent<WeaponChange>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadFinished()
    {
        weaponchange.ReturnRigWeightToOne();
    }
}
