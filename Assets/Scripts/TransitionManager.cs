using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;

    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void TransationToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
