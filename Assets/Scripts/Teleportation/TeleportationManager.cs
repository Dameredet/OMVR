using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationManager : MonoBehaviour
{
    [SerializeField]
    InteractorsManagement interactorsManagement;

    [SerializeField]
    private InputActionAsset actionAsset;

    [SerializeField]
    private XRRayInteractor interactor;

    [SerializeField]
    private TeleportationProvider provider;

    private InputAction thumbstick;

    private bool isActive;
    private bool thumbstickReleased;


    private void Start()
    {
        TurnOn();
    }
    public void TurnOff()
    {
        interactor.enabled = false;

        var activate = actionAsset.FindActionMap("XRI RightHand Locomotion").FindAction("Teleport Mode Activate");
        activate.Disable();

        var cancel = actionAsset.FindActionMap("XRI RightHand Locomotion").FindAction("Teleport Mode Cancel");
        cancel.Disable();

        thumbstick = actionAsset.FindActionMap("XRI RightHand Locomotion").FindAction("Move");
        thumbstick.Disable();
    }
    public void TurnOn()
    {
        interactor.enabled = false;

        var activate = actionAsset.FindActionMap("XRI RightHand Locomotion").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += OnTeleportationActivate;

        var cancel = actionAsset.FindActionMap("XRI RightHand Locomotion").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        cancel.performed += OnTeleportationCancel;

        thumbstick = actionAsset.FindActionMap("XRI RightHand Locomotion").FindAction("Move");
        thumbstick.Enable();
        thumbstick.canceled += OnThumbstickReleased;
    }
    void Update()
    {
        if (!isActive || !thumbstickReleased)
        {
            return;
        }

        if(thumbstick.triggered)
        {
            return;
        }

        if(!interactor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            interactor.enabled = false;
            isActive = false;
            return;
        }

        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point
        };

        provider.QueueTeleportRequest(request);

        interactor.enabled = false;
        isActive = false;
        thumbstickReleased = false;
    }


    private void OnTeleportationActivate(InputAction.CallbackContext context)
    {
        if(!isActive) 
        {
            interactor.enabled = true;
            isActive = true;
        //    interactorsManagement.TurnOffRayInteractors();
        }

    }

    private void OnTeleportationCancel(InputAction.CallbackContext context)
    {
        if(isActive && interactor.enabled == true) 
        { 
        interactor.enabled = false;
        isActive = false;
        //    interactorsManagement.TurnOnRayInteractors();
        }

    }

    private void OnThumbstickReleased(InputAction.CallbackContext context)
    {
        thumbstickReleased = true;
       // interactorsManagement.TurnOnRayInteractors();
    }

    private void OnDestroy()
    {
        Debug.Log("Distroyed Teleporter");
    }
}
