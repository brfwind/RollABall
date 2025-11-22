using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button reTry;
    public Button menu;
    public Button nextLevel;

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadNextLevel()
    {
        string cur = SceneManager.GetActiveScene().name;
        int level = (int)char.GetNumericValue(cur[cur.Length - 1]);
        level += 1;
        string nextLevelName = "Level_" + level;
        
        SceneManager.LoadScene(nextLevelName);
    }
  
}
