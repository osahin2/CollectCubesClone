using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float lerpFactor;
    [SerializeField]
    private RectTransform rectImage;
    [SerializeField]
    private Rigidbody rb;

    private Vector3 move;
    private Vector2 firstPos;
    private Vector2 dragPos;

    public void Initialized()
    {
        InputEventHandler.PointerDragged += Move;
        InputEventHandler.PointerDowned += FingerDown;
    }
    
    private void Move(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectImage, eventData.position, eventData.pressEventCamera, out dragPos);

        if (eventData.dragging)
        {
            var delta = dragPos - firstPos;
            move = new Vector3(delta.x, 0, delta.y);
            rb.velocity = move * playerSpeed * Time.fixedDeltaTime;
        }

        if (move != Vector3.zero)
        {
            transform.forward = move;
            transform.forward = Vector3.Lerp(transform.forward, move, lerpFactor * Time.fixedDeltaTime);
        }
    }
    private void FingerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectImage, eventData.position, eventData.pressEventCamera, out firstPos);
    }

    public void StopPlayerInput()
    {
        InputEventHandler.PointerDragged -= Move;
        InputEventHandler.PointerDowned -= FingerDown;
    }
}
