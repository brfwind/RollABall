using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject escPanel;
    public Button exitGame;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(escPanel.activeInHierarchy)
            {
                escPanel.SetActive(false);
            }
            else
            {
                escPanel.SetActive(true);
            }
        }
    }

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();

        if (!PlayerPrefs.HasKey("UnLockedLevelIndex"))
        {
            PlayerPrefs.SetInt("UnLockedLevelIndex", 1);
            PlayerPrefs.Save();
        }
    }

    public void GoToLevel(string name)
    {
        TransitionManager.instance.Transition(name);
    }

    public void ExitGame()
    {
        Debug.Log("退出游戏");
        Application.Quit();
    }
}
