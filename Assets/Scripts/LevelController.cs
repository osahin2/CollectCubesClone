using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    private float cubeCounter = 0;

    private void Awake()
    {
        CubeEffector.Instance.OnEnterArea += HandleLevelEnd;
    }

    private void HandleLevelEnd(CubeEffector cubeEffector)
    {
        cubeCounter++;
        UIProgressBar.Instance.SetProgressValue(cubeCounter/ transform.childCount);
        if (cubeCounter == transform.childCount)
        {
            GameController.Instance.CoinCounter(transform.childCount);
            cubeCounter = 0;
            GameController.Instance.LevelEnd();
        }
    }

    private void OnDestroy()
    {
        CubeEffector.Instance.OnEnterArea -= HandleLevelEnd;
    }
}
