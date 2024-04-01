using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentObject : MonoBehaviour
{
    public Vector3 GetPosition(Content museumObject)
    {
        Vector3 position = new Vector3(museumObject.positionx, museumObject.positiony, museumObject.positionz);

        return position;
    }

    public Quaternion GetRotation(Content museumObject)
    {
        Quaternion rotation = Quaternion.Euler(museumObject.rotationx, museumObject.rotationy, museumObject.rotationz);

        return rotation;
    }

    public Vector3 GetScale(Content museumObject)
    {
        Vector3 scale = new Vector3(museumObject.scalex, museumObject.scaley, museumObject.scalez);

        return scale;
    }
    public void SetTransform(Content museumObject, Transform transform)
    {
        museumObject.positionx = transform.position.x;
        museumObject.positiony = transform.position.y;
        museumObject.positionz = transform.position.z;

        museumObject.rotationx = transform.rotation.eulerAngles.x;
        museumObject.rotationy = transform.rotation.eulerAngles.y;
        museumObject.rotationz = transform.rotation.eulerAngles.z;

        museumObject.scalex = transform.localScale.x;
        museumObject.scaley = transform.localScale.y;
        museumObject.scalez = transform.localScale.z;
    }
}
