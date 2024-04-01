using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorsManagement : MonoBehaviour
{
    [SerializeField]
    TeleportationManager teleportationManager;

    [SerializeField]
    GameObject RayInteractorLeft;
    [SerializeField]
    GameObject RayInteractorRight;
    public void TurnOffTeleportation()
    {
        teleportationManager.TurnOff();
    }

    public void TurnOnTeleportation()
    {
        teleportationManager.TurnOn();
    }

    public void TurnOffRayInteractors()
    {
        RayInteractorLeft.SetActive(false);
        RayInteractorRight.SetActive(false);
    }

    public void TurnOnRayInteractors()
    {
        RayInteractorLeft.SetActive(true);
        RayInteractorRight.SetActive(true);
    }
}
