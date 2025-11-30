using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject escPanel;
    public Button exitGame;
    public static bool is3DCamera = false;
    public Toggle toggle;

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

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();

        if (!PlayerPrefs.HasKey("UnLockedLevelIndex"))
        {
            PlayerPrefs.SetInt("UnLockedLevelIndex", 1);
            PlayerPrefs.Save();
        }

        toggle.isOn = is3DCamera;
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

    public void Set3DCamera(bool on)
    {
        is3DCamera = on;
    }
}
