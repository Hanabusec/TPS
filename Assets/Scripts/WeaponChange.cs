using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponChange : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private Animator anim;
    private Rig rig;
    
    [SerializeField] Transform[] weaponTransforms;

    [SerializeField] Transform pistol;
    [SerializeField] Transform revolver;
    [SerializeField] Transform rifle;
    [SerializeField] Transform shotgun;
    [SerializeField] Transform sniperRifle;

    private Transform currentGun; 

    [Header("LeftHandIK")]
    [SerializeField] private Transform leftHand_IK_Target;
    
    [Header("Rig")]
    [SerializeField] private float rigWeightIncreaseRate;
    private bool isRigWeightIncreasing;
    
    void Start()
    {
        //anim = GetComponentInParent<Animator>();
        anim = GetComponentInChildren<Animator>();
        rig = GetComponentInChildren<Rig>();
        
        //Debug.Log("sdsadsada");
        SwitchOnWeapons(pistol);
    }

    // Update is called once per frame
    void Update()
    {
        CheckWeaponSwitch();

        if(Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("Reload");
            PauseRig();
            
        }

        //if(Input.GetKey(KeyCode.K))
        //{
           // rig.weight += rigWeightIncreaseRate * Time.deltaTime;
        
        //}

        if(isRigWeightIncreasing)
        {
            rig.weight += rigWeightIncreaseRate * Time.deltaTime;

            if(rig.weight >= 1)
            {
                isRigWeightIncreasing = false;
            }
        }

        
    }

    private void PauseRig()
    {
        rig.weight = 0.15f;
    }

    public void ReturnRigWeightToOne()
    {
        isRigWeightIncreasing = true;
    }

    private void AttachLeftHand()
    {
        Transform targetTransform = currentGun.GetComponentInChildren<LeftHandTransform>().transform;
        
        leftHand_IK_Target.localPosition = targetTransform.localPosition;
        leftHand_IK_Target.localRotation = targetTransform.localRotation;
    }

    private void SwitchAnimationLayer(int layerIndex)
    {
        for(int i =1; i <anim.layerCount; i ++)
        {
            anim.SetLayerWeight(i,0);
        }

        anim.SetLayerWeight(layerIndex, 1);
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
        currentGun = weaponTransform;

        AttachLeftHand();
    }

    private void CheckWeaponSwitch()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchOnWeapons(pistol);
            SwitchAnimationLayer(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchOnWeapons(revolver);
            SwitchAnimationLayer(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchOnWeapons(rifle);
            SwitchAnimationLayer(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchOnWeapons(shotgun);
            SwitchAnimationLayer(2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchOnWeapons(sniperRifle);
            SwitchAnimationLayer(3);
        }
    }
}
