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
    private Image onScreenImage;
    [SerializeField]
    private InputEventHandler inputHandler;

    private Vector3 move;
    private Vector2 firstPos;
    private Vector2 dragPos;
    private Rigidbody rb;
    
    public void Initialized()
    {
        inputHandler.PointerDragged += Move;
        inputHandler.PointerDowned += FingerDown;
    }

    private void Awake()
    {
        Initialized();
        rb = GetComponent<Rigidbody>();
    }

    private void Move(PointerEventData eventData)
    {
        if (onScreenImage.TryGetComponent(out RectTransform rectImage))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectImage, eventData.position, eventData.pressEventCamera, out dragPos);
        }

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
        if (onScreenImage.TryGetComponent(out RectTransform rectImage))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectImage, eventData.position, eventData.pressEventCamera, out firstPos);
        }
    }

    public void StopPlayerInput()
    {
        inputHandler.PointerDragged -= Move;
        inputHandler.PointerDowned -= FingerDown;
    }
}
