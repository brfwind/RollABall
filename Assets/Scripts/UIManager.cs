using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button reTry;
    public Button menu;
    public Button nextLevel;
    public GameObject overPanel;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI levelName;

    //重玩按钮
    public void RestartGame()
    {
        TransitionManager.instance.Transition(SceneManager.GetActiveScene().name);
    }

    //返回选关按钮
    public void BackToMenu()
    {
        TransitionManager.instance.Transition("Menu");
    }

    //下一关按钮
    public void LoadNextLevel()
    {
        string cur = SceneManager.GetActiveScene().name;
        int level = (int)char.GetNumericValue(cur[cur.Length - 1]);
        
        if(++level <= PlayerPrefs.GetInt("UnLockedLevelIndex"))
        {
            nextLevel.interactable = true;
            string nextLevelName = "Level_" + level;
            TransitionManager.instance.Transition(nextLevelName);
        }else
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

    public void SetWinTextAndColor(string content,Color color)
    {
        winText.text = content;
        winText.color = color;
    }
  
}
