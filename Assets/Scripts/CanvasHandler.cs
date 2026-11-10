using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour
{
    [SerializeField] bool IsGameStart;
    [SerializeField] Button PlayButton;
    [SerializeField] Button PauseButton;
    [SerializeField] GameObject HudCanvas;
    [SerializeField] GameObject MainMenuCanvas;
    [SerializeField] PlayerController playerController;
    // Start is called before the first frame update
    void OnEnable()
    {
        // PlayButton.onClick.AddListener(() => GameStart());
        // PauseButton.onClick.AddListener(() => GamePause());
    }
    void OnDisable()
    {
        // PlayButton.onClick.RemoveListener(() => GameStart());
        // PauseButton.onClick.RemoveListener(() => GamePause());
    }

    private void GameStart()
    {
        if (!IsGameStart)
        {
            IsGameStart = true;
            HudCanvas.SetActive(true);
            playerController.enabled = true;
            MainMenuCanvas.SetActive(false);
        }
    }
    private void GamePause()
    {
        if (IsGameStart)
        {
            IsGameStart = false;
            HudCanvas.SetActive(false);
            MainMenuCanvas.SetActive(true);
            playerController.enabled = false;
        }
    }

    void Start()
    {
        HudCanvas.SetActive(false);
        playerController.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsGameStart && Input.GetKeyDown(KeyCode.Space))
        {
            HudCanvas.SetActive(true);
            playerController.enabled = true;
            MainMenuCanvas.SetActive(false);
        }
        if (IsGameStart && Input.GetKeyDown(KeyCode.Escape))
        {
            HudCanvas.SetActive(false);
            MainMenuCanvas.SetActive(true);
            playerController.enabled = false;
        }
    }
}
