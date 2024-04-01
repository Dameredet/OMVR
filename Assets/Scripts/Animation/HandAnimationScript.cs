using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimationScript : MonoBehaviour
{
    public InputActionProperty pinchAnimation;
    public InputActionProperty gripAnimation;

    public Animator handAnimator;
    
    void Update()
    {
        float pinchValue = pinchAnimation.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", pinchValue);

        float gripValue = gripAnimation.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
 
    }
}
