using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
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

}
