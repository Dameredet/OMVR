using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.InputSystem;

public class RecenterManager : MonoBehaviour
{
    public Transform head;
    public Transform origin;
    public Transform target;

    public InputActionProperty recenterButton;
    public void Recenter()
    {
        
        Vector3 offset = head.position- origin.position;
        offset.y = 0f;
        origin.position = target.position - offset;

        Vector3 targetForward = target.forward;
        targetForward.y = 0f;
        Vector3 cameraForward = head.forward;
        cameraForward.y = 0f;
        float angle = Vector3.SignedAngle(cameraForward, targetForward, Vector3.up);
        origin.RotateAround(head.position, Vector3.up, angle);
/*
        XROrigin origin = GetComponent<XROrigin>();
        origin.MoveCameraToWorldLocation(target.position);
        origin.MatchOriginUpCameraForward(target.up, target.forward);*/
    }
    void Update()
    {
        if(recenterButton.action.WasPressedThisFrame()) Recenter();
    }
}
