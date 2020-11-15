using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("Others")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform levelPos;
    [SerializeField] private Renderer[] playerRend;
    [SerializeField] private Material[] playerMats;

    [Header("UI Elements")]
    [SerializeField] private Text dragToStartText;
    [SerializeField] private Image levelEndScreen;
    [SerializeField] private Text levelEndText;
    [SerializeField] private Button retryLevelButton;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private Text progressInstantLevelText;
    [SerializeField] private Text progresNextLevelText;
    [SerializeField] private Text coinText;
    [SerializeField] private Button skinButton;

    [Header("Particle")]
    [SerializeField] private ParticleSystem levelEndParticle;
    [Header("Levels")]
    [SerializeField] List<GameObject> levels;
    
    private int index = -1;
    private bool fadeControl;
    private Vector3 playerStartPos;
    private Quaternion playerRot;
    private int instantLevel;
    private int progressNextLevel;
    private int playerSave;

    private float coins;
    public float Coins => coins;


    private GameObject level;
    public GameObject Level => level;

    private void Initialized()
    {
        Instance = this;

        playerController.Initialized();
        InputEventHandler.PointerDragged += DragToStart;
    }

    private void Awake()
    {
        Initialized();
    }

    private void Start()
    {
        playerStartPos = playerController.transform.position;
        playerRot = Quaternion.identity;

        LeanTween.alphaText(dragToStartText.rectTransform, 0f, 1.5f)
            .setEase(LeanTweenType.easeInCirc).setLoopPingPong();

        GetSave();
        GetLevel();
        coins = 1000;
    }
    
    private void GetLevel()
    {
        index++;
        if (index >= levels.Count)
        {
            index = 0;
        }
        level = Instantiate(levels[index], levelPos.position, Quaternion.identity);
        progressNextLevel = index + 1;
        progressInstantLevelText.text = index.ToString();
        progresNextLevelText.text = progressNextLevel.ToString();
    }

    private void DragToStart(PointerEventData eventData)
    {
        dragToStartText.enabled = false;
        retryLevelButton.gameObject.SetActive(true);
        skinButton.gameObject.SetActive(false);
    }

    public void LevelEnd()
    {
        instantLevel = index + 1;
        InputEventHandler.PointerDragged -= DragToStart;
        playerController.StopPlayerInput();
        retryLevelButton.gameObject.SetActive(false);
        LeanTween.alpha(levelEndScreen.rectTransform, 1f, 1f).setEase(LeanTweenType.easeInCirc).setOnStart(()=> {
            levelEndText.enabled = true;
            levelEndText.text = "LEVEL " + instantLevel + " COMPLETED";
            progressBar.SetActive(false);
            UIProgressBar.Instance.SetProgressValue(0f);
            levelEndParticle.Play();
        }).setOnComplete(() => {
            levelEndText.enabled = false;
            Destroy(level);
            ResetPlayerPosition();
            GetLevel();
            LevelStart();
        });
    }

    private void LevelStart()
    {
        LeanTween.alpha(levelEndScreen.rectTransform, 0f, 1f).setEase(LeanTweenType.easeInCirc).setOnComplete(() => {
            InputEventHandler.PointerDragged += DragToStart;
            playerController.Initialized();
            dragToStartText.enabled = true;
            progressBar.SetActive(true);
            skinButton.gameObject.SetActive(true);
        });
    }

    public void RetryLevel()
    {
        ResetPlayerPosition();
        if (playerController.TryGetComponent(out Rigidbody playerRb))
        {
            playerRb.velocity = Vector3.zero;
        }

        Destroy(level);
        level = Instantiate(levels[index], levelPos.position, Quaternion.identity);

        dragToStartText.enabled = true;
        retryLevelButton.gameObject.SetActive(false);
        UIProgressBar.Instance.SetProgressValue(0f);
        playerController.gameObject.SetActive(true);
        skinButton.gameObject.SetActive(true);
    }

    private void ResetPlayerPosition()
    {
        playerController.transform.position = playerStartPos;
        playerController.transform.rotation = playerRot;
    }

    public void CoinCounter(float value)
    {
        coins += value;
        coinText.text = coins.ToString();
        PlayerPrefs.SetFloat("coinSave", coins);
    }

    public void SelectPlayer(int matValue)
    {
        for (int i = 0; i < playerRend.Length; i++)
        {
            playerRend[i].material = playerMats[matValue];
            playerSave = matValue;
            PlayerPrefs.SetInt("savePlayer", playerSave);
        }
    }

    private void GetSave()
    {
        coins = PlayerPrefs.GetFloat("coinSave");
        coinText.text = coins.ToString();

        playerSave = PlayerPrefs.GetInt("savePlayer");
        for (int i = 0; i < playerRend.Length; i++)
        {
            playerRend[i].material = playerMats[playerSave];
        }
    }

    public void BuyPlayerSkin(float spendCoin)
    {
        coins -= spendCoin;
        coinText.text = coins.ToString();
        Debug.Log("Spend Coin");
    }
}
