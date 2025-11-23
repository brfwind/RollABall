using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void GoToLevel(string name)
    {
        TransitionManager.instance.Transition(name);
    }
}
