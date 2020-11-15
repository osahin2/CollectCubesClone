using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressBar : MonoBehaviour
{
    public UIProgressBar()
    {
        instance = this;
    }

    private static UIProgressBar instance;

    public static UIProgressBar Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIProgressBar();
            }
            return instance;
        }
    }

    [SerializeField] private Image progressMask;

    float originalSize;

    public void SetProgressValue(float value)
    {
        if (gameObject.TryGetComponent(out RectTransform rectTransform))
        {
            originalSize = rectTransform.rect.width;
        }
        progressMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value );
    }
}
