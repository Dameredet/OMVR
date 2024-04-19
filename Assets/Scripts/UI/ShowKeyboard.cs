using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using System;
using Unity.XR.CoreUtils;
using UnityEngine.EventSystems;

public class ShowKeyboard : MonoBehaviour
{
    private TMP_InputField inputField;

    private float Distance = 0.50f;
    private float verticalOffset = -0.25f;

    public Transform transform;
    private bool keyboardOpen = false;

    void Start()
    {
        inputField= GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(x => OpenKeyboard());

        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { OnPointerClick((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    public void OpenKeyboard()
    {

        NonNativeKeyboard.Instance.InputField= inputField;
        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);

        Vector3 direction = transform.forward;
        direction.y= 0;
        direction.Normalize();

        Vector3 target = transform.position + direction * Distance + Vector3.up * verticalOffset;

        NonNativeKeyboard.Instance.RepositionKeyboard(target);
        SetUpCaret(1);

        NonNativeKeyboard.Instance.OnClosed += KeyboardOnClosed;
    }

    private void KeyboardOnClosed(object sender, EventArgs e)
    {
        keyboardOpen = false;
        SetUpCaret(0);
        NonNativeKeyboard.Instance.OnClosed -= KeyboardOnClosed;
    }

    public void SetUpCaret(float value)
    {
        inputField.customCaretColor = true;
        Color color = inputField.caretColor;
        color.a = value;
        inputField.caretColor = color;
    }


    private void OnPointerClick(PointerEventData eventData)
    {
        if (keyboardOpen)
        {
            if (eventData.pointerCurrentRaycast.gameObject != NonNativeKeyboard.Instance.gameObject)
            {
                NonNativeKeyboard.Instance.Close();
            }
        }
    }
}
