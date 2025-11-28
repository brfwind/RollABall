using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("GameOverPanel")]
    public Button reTry;
    public Button menu;
    public Button nextLevel;

    [Header("EscPanel")]
    public Button continueB;
    public Button menu1;
    public Button exit;

    [Header("PlayPanel")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI levelName;

    [Header("Panels")]
    public GameObject overPanel;
    public GameObject escPanel;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escPanel.activeInHierarchy)
            {
                StartCoroutine(TransitionManager.instance.ClosePanel(escPanel));
            }
            else
            {
                TransitionManager.instance.ShowPanel(escPanel);
            }
        }
    }

    //重玩按钮
    public void RestartGame()
    {
        if (escPanel.activeInHierarchy == true)
        {
            escPanel.SetActive(false);
            Time.timeScale = 1;
        }

        TransitionManager.instance.Transition(SceneManager.GetActiveScene().name);
    }

    //返回选关按钮
    public void BackToMenu()
    {
        Time.timeScale = 1;
        TransitionManager.instance.Transition("Menu");
    }

    public void ContinueB()
    {
        Time.timeScale = 1;
        escPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("退出游戏");
        Application.Quit();
    }

    //下一关按钮
    public void LoadNextLevel()
    {
        string cur = SceneManager.GetActiveScene().name;
        int level = (int)char.GetNumericValue(cur[cur.Length - 1]);

        if (++level <= PlayerPrefs.GetInt("UnLockedLevelIndex"))
        {
            nextLevel.interactable = true;
            string nextLevelName = "Level_" + level;
            TransitionManager.instance.Transition(nextLevelName);
        }
        else
        {
            nextLevel.interactable = false;
        }
    }

    public void SetTextColor(Color color)
    {
        timeText.color = color;
    }

    public void SetTimeText(float timer)
    {
        timeText.text = "Time: " + timer.ToString("F2");
    }

    public void ShowOverPanel()
    {
        overPanel.SetActive(true);
    }

    public void SetLevelText(string name)
    {
        levelName.text = name;
    }

    public void SetWinTextAndColor(string content, Color color)
    {
        winText.text = content;
        winText.color = color;
    }
}
