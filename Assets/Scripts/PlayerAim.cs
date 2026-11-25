using System;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Player player;

    private PlayerControls playerControls;

    private Vector2 aimInput;

    [Header("Camera Parameters")]
    [Range(.5f, 1f)]
    [SerializeField] private float minCameraDistance = 1f;
    [Range(1, 5f)]
    [SerializeField] private float maxCameraDistance = 3f;
    [Range(3f, 6f)]
    [SerializeField] private float cameraSensitivity = 4f;

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
        //aimTarget.position = GetMousePosition();
        //aimTarget.position = new Vector3(aimTarget.position.x, transform.position.y + .6f, aimTarget.position.z);

        aimTarget.position = Vector3.Lerp(aimTarget.position, CameraAimPosition(), cameraSensitivity * Time.deltaTime);
    }

    private Vector3 CameraAimPosition()
    {
        Vector3 cameraAimPosition = GetMousePosition();
        Vector3 cameraAimDirection = (cameraAimPosition - transform.position).normalized;

        float distanceToCameraPosition = Vector3.Distance(transform.position, cameraAimPosition);

        float clampedDistance = Mathf.Clamp(distanceToCameraPosition, minCameraDistance, maxCameraDistance);

        // if (distanceToCameraPosition > maxCameraDistance)
        // {
        //     cameraAimPosition = transform.position + cameraAimDirection * maxCameraDistance;
        // }
        // else if (distanceToCameraPosition < minCameraDistance)
        // {
        //     cameraAimPosition = transform.position + cameraAimDirection * minCameraDistance;
        // }

        cameraAimPosition = transform.position + cameraAimDirection * clampedDistance;
        cameraAimPosition.y = transform.position.y + .6f;

        return cameraAimPosition;
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
