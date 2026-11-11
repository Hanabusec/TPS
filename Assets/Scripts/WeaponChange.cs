using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] Transform[] weaponTransforms;

    [SerializeField] Transform pistol;
    [SerializeField] Transform revolver;
    [SerializeField] Transform rifle;
    [SerializeField] Transform shotgun;
    [SerializeField] Transform sniperRifle;
    
    void Start()
    {
        //Debug.Log("sdsadsada");
        SwitchOnWeapons(pistol);
    }

    // Update is called once per frame
    void Update()
    {
        CheckWeaponSwitch();
    }

    private void SwitchOffWeapons()
    {
        for(int i = 0 ; i < weaponTransforms.Length; i ++)
        {
            weaponTransforms[i].gameObject.SetActive(false);
        }
    }

    private void SwitchOnWeapons(Transform weaponTransform)
    {
        SwitchOffWeapons();
        weaponTransform.gameObject.SetActive(true);
    }

    private void CheckWeaponSwitch()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchOnWeapons(pistol);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchOnWeapons(revolver);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchOnWeapons(rifle);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchOnWeapons(shotgun);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchOnWeapons(sniperRifle);
        }
    }
}
