using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class AdjustmentUI : MonoBehaviour
{
    public float ButtonAdjustmentUnit = 0.01f;

    [SerializeField]
    Slider SliderX;
    public float MinX = -2;
    public float MaxX = 2;
    [SerializeField]
    Slider SliderY;
    public float MinY = 1.09f;
    public float MaxY = 4.8f;
    [SerializeField]
    Slider SliderZ;
    public float MinZ = -1;
    public float MaxZ = 1;

    [SerializeField]
    Slider RotationX;
    [SerializeField]
    Slider RotationY;
    [SerializeField]
    Slider RotationZ;
    public float MinRotation = 0f;
    public float MaxRotation = 360f;

    [SerializeField]
    TMP_InputField ScaleX;
    [SerializeField]
    TMP_InputField ScaleY;
    [SerializeField]
    TMP_InputField ScaleZ;

    private GameObject museumObject;
    float result;
    void Start()
    {
        SliderX.minValue = MinX;
        SliderX.maxValue = MaxX;

        SliderY.minValue = MinY;
        SliderY.maxValue = MaxY;

        SliderZ.minValue = MinZ;
        SliderZ.maxValue = MaxZ;

        SliderX.onValueChanged.AddListener(SliderXOnValueChanged);
        SliderY.onValueChanged.AddListener(SliderYOnValueChanged);
        SliderZ.onValueChanged.AddListener(SliderZOnValueChanged);

        RotationX.minValue = MinRotation;
        RotationX.maxValue = MaxRotation;

        RotationY.minValue = MinRotation;
        RotationY.maxValue = MaxRotation;

        RotationZ.minValue = MinRotation;
        RotationZ.maxValue = MaxRotation;

        RotationX.onValueChanged.AddListener(RotationXOnInputValueChanged);
        RotationY.onValueChanged.AddListener(RotationYOnInputValueChanged);
        RotationZ.onValueChanged.AddListener(RotationZOnInputValueChanged);

        ScaleX.onValueChanged.AddListener(ScaleXOnInputValueChanged);
        ScaleY.onValueChanged.AddListener(ScaleYOnInputValueChanged);
        ScaleZ.onValueChanged.AddListener(ScaleZOnInputValueChanged);
    }

    public void SetMuseumObject(GameObject mo)
    {
        museumObject = mo;
        SetInitialSliderValue(museumObject.transform.position.x, SliderX);
        SetInitialSliderValue(museumObject.transform.position.y, SliderY);
        SetInitialSliderValue(museumObject.transform.position.z, SliderZ);

        SetInitialSliderValue(museumObject.transform.rotation.x, RotationX);
        SetInitialSliderValue(museumObject.transform.rotation.y, RotationY);
        SetInitialSliderValue(museumObject.transform.rotation.z, RotationZ);

        ScaleX.text = museumObject.transform.localScale.x.ToString();
        ScaleY.text = museumObject.transform.localScale.y.ToString();
        ScaleZ.text = museumObject.transform.localScale.z.ToString();
    }

    public void ButtonCloser()
    {
        Vector3 newPositionVector = museumObject.transform.position;
        newPositionVector.z = newPositionVector.z - ButtonAdjustmentUnit;
        museumObject.transform.position = newPositionVector;
    }

    public void ButtonFurther()
    {
        Vector3 newPositionVector = museumObject.transform.position;
        newPositionVector.z = newPositionVector.z + ButtonAdjustmentUnit;
        museumObject.transform.position = newPositionVector;
    }

    public void ButtonRight()
    {
        Vector3 newPositionVector = museumObject.transform.position;
        newPositionVector.x = newPositionVector.x + ButtonAdjustmentUnit;
        museumObject.transform.position = newPositionVector;
    }

    public void ButtonLeft()
    {
        Vector3 newPositionVector = museumObject.transform.position;
        newPositionVector.x = newPositionVector.x - ButtonAdjustmentUnit;
        museumObject.transform.position = newPositionVector;
    }

    public void ButtonUp()
    {
        Vector3 newPositionVector = museumObject.transform.position;
        newPositionVector.y = newPositionVector.y + ButtonAdjustmentUnit;
        museumObject.transform.position = newPositionVector;
    }

    public void ButtonDown()
    {
        Vector3 newPositionVector = museumObject.transform.position;
        newPositionVector.y = newPositionVector.y - ButtonAdjustmentUnit;
        museumObject.transform.position = newPositionVector;
    }

    void SetInitialSliderValue(float newValue, Slider slider)
    {
        if (newValue < slider.maxValue)
        {
            if(newValue > slider.minValue)
            {
                slider.value = newValue;
            }
            else slider.value = slider.minValue;
        }
        else slider.value= slider.maxValue;
    }


    bool ValidTextFieldValue(string newValue)
    {

        if (float.TryParse(newValue, out result))
        {
            return true;
        }
        return false;
    }
    void SliderXOnValueChanged(float newValue)
    {

            Vector3 newPositionVector = museumObject.transform.position;
            newPositionVector.x = newValue;
            museumObject.transform.position = newPositionVector;
        
    }
    void SliderYOnValueChanged(float newValue)
    {

        Vector3 newPositionVector = museumObject.transform.position;
        newPositionVector.y = newValue;
        museumObject.transform.position = newPositionVector;

    }
    void SliderZOnValueChanged(float newValue)
    {

        Vector3 newPositionVector = museumObject.transform.position;
        newPositionVector.z = newValue;
        museumObject.transform.position = newPositionVector;

    }


    void RotationXOnInputValueChanged(float newValue)
    {
        Quaternion currentRotation = museumObject.transform.rotation;
        float currentY = currentRotation.eulerAngles.y;
        float currentZ = currentRotation.eulerAngles.z;
        museumObject.transform.rotation = Quaternion.Euler(newValue, currentY, currentZ);

    }
    void RotationYOnInputValueChanged(float newValue)
    {
        Quaternion currentRotation = museumObject.transform.rotation;
        float currentX = currentRotation.eulerAngles.x;
        float currentZ = currentRotation.eulerAngles.z;
        museumObject.transform.rotation = Quaternion.Euler(currentX, newValue, currentZ);
    }
    void RotationZOnInputValueChanged(float newValue)
    {
        Quaternion currentRotation = museumObject.transform.rotation;
        float currentX = currentRotation.eulerAngles.x;
        float currentY = currentRotation.eulerAngles.y;
        museumObject.transform.rotation = Quaternion.Euler(currentX, currentY, newValue);
    }
    void ScaleXOnInputValueChanged(string newValue)
    {
        if (ValidTextFieldValue(newValue))
        {
            Vector3 currentScale = museumObject.transform.localScale;
            museumObject.transform.localScale = new Vector3(result, currentScale.y, currentScale.z);
        }
    }
    void ScaleYOnInputValueChanged(string newValue)
    {
        if (ValidTextFieldValue(newValue))
        {
            Vector3 currentScale = museumObject.transform.localScale;
            museumObject.transform.localScale = new Vector3(currentScale.x, result, currentScale.z);
        }
    }
    void ScaleZOnInputValueChanged(string newValue)
    {
        if (ValidTextFieldValue(newValue))
        {
            Vector3 currentScale = museumObject.transform.localScale;
            museumObject.transform.localScale = new Vector3(currentScale.x, currentScale.y, result);
        }
    }
}
