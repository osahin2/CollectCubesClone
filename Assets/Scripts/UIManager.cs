using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private const float SPEND_COIN = 100.0f;

    [SerializeField] Button[] playerSelectButtons;
    [SerializeField] Image[] playerUnknownImages;

    private int randomSkinVal;
    private int randomVal = 8;

    public void OpenRandomPlayerSkin()
    {
        randomSkinVal = Random.Range(0, randomVal);

        if (GameController.Instance.Coins > 100 && !playerSelectButtons[randomSkinVal].interactable)
        {
            OpenButtons(randomSkinVal);
        }
        else
        {
            for (int i = 0; i < playerSelectButtons.Length; i++)
            {
                if (!playerSelectButtons[i].interactable && GameController.Instance.Coins > 100)
                {
                    OpenButtons(i);
                    break;
                }
            }
        }
    }

    private void OpenButtons(int val)
    {
        playerSelectButtons[val].interactable = true;
        playerUnknownImages[val].enabled = false;
        GameController.Instance.BuyPlayerSkin(SPEND_COIN);

    }
}
