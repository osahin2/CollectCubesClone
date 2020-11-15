using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputEventHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public static event Action<PointerEventData> PointerDowned;
    public static event Action<PointerEventData> PointerDragged;
    public static event Action<PointerEventData> PointerUpped;

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDowned?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        PointerDragged?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerUpped?.Invoke(eventData);
    }
}
