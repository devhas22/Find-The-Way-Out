using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ViewPenal : MonoBehaviour
{
    [SerializeField] GameObject ControlsText;
    [SerializeField] GameObject ProgressText;
    [SerializeField] GameObject ObjectivesText;
    void Start()
    {
        ControlsText.SetActive(false);
        ProgressText.SetActive(false);
        ObjectivesText.SetActive(false);
    }

    public void ShowObjectives()
    {
        ObjectivesText.SetActive(true);
    }
    public void HideObjectives()
    {
        ObjectivesText.SetActive(false);
    }
    public void ShowControls()
    {
        ControlsText.SetActive(true);
    }
    public void HideControls()
    {
        ControlsText.SetActive(false);
    }
    public void ShowProgress()
    {
        ProgressText.SetActive(true);
    }
    public void HideProgress()
    {
        ProgressText.SetActive(false);
    }

}
