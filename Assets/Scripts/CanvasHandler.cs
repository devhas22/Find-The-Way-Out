using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
    [SerializeField] GameObject HudCanvas;
    [SerializeField] GameObject MainMenuCanvas;
    [SerializeField] PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        HudCanvas.SetActive(false);
        playerController.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HudCanvas.SetActive(true);
            playerController.enabled = true;
            MainMenuCanvas.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HudCanvas.SetActive(false);
            MainMenuCanvas.SetActive(true);
            playerController.enabled = false;
        }
    }
}
