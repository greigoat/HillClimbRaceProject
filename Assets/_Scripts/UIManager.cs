using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Text timerText;
    public Text raceTimeText;
    public GameObject pauseButton;

    public RectTransform activeMenu { get; private set; }

    public void DrawFinishTime()
    {
        raceTimeText.text += timerText.text;
    }

    public static void DrawTimer(float timer, bool b)
    {
        Instance.timerText.gameObject.SetActive(b);
        Instance.timerText.text = timer.ToString("0.0");
    }

    public void SetIntermediateMenu(RectTransform menu)
    {
        if (activeMenu == menu)
        {
            menu.gameObject.SetActive(!menu.gameObject.activeSelf);
            return;
        }

        menu.gameObject.SetActive(true);
        DeactivateActiveMenu();
        activeMenu = menu;

        /*
                finishRaceMenuRect.gameObject.SetActive(true);
                GameplayManager.Instance.IsTimerActive = false;
                DrawRaceTimeText();
                activeMenu = finishRaceMenuRect;
                break;
                */
    }

    public void DeactivateActiveMenu()
    {
        if (activeMenu)
            activeMenu.gameObject.SetActive(false);
    }

    // Use this for initialization
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
    }
}
