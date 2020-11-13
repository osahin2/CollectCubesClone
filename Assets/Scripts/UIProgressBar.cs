using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressBar : MonoBehaviour
{
    public static UIProgressBar Instance { get; private set; }

    [SerializeField] private Image progressMask;

    float originalSize;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        originalSize = gameObject.GetComponent<RectTransform>().rect.width;
    }

    public void SetProgressValue(float value)
    {
        progressMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value );
    }
}
