using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;
    public float speed;
    private CanvasGroup canvasGroup;

    //单例模式
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    //进游戏，渐变白
    void Start()
    {
        StartCoroutine(Fade(0));
    }

    public void Transition(string sceneName)
    {
        StartCoroutine(TransitionToScene(sceneName));
    }

    private IEnumerator TransitionToScene(string sceneName)
    {
        yield return Fade(1);
        yield return SceneManager.LoadSceneAsync(sceneName);
        yield return Fade(0);
    }

    private IEnumerator Fade(int goal)
    {
        //拦截输入
        canvasGroup.blocksRaycasts = true;

        //实现渐变
        while(Mathf.Abs(canvasGroup.alpha - goal) > 0.01f)
        {
            switch(goal)
            {
                case 1:
                    canvasGroup.alpha += Time.deltaTime * speed;
                    break;
                case 0:
                    canvasGroup.alpha -= Time.deltaTime * speed;
                    break;
            }

            yield return null;
        }

        canvasGroup.blocksRaycasts = false;
    }


}
