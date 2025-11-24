using System;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public LevelData levelData;
    public UIManager ui;
    private int totalFoods;
    private int score = 0;
    private float timer = 0f;
    private bool started = false;
    private bool finished = false;

    public event Action OnWinGame;

    //获取食物数量
    //获取关卡名称
    //订阅事件
    void Start()
    {
        totalFoods = GameObject.FindGameObjectsWithTag("Food").Length;
        ui.SetLevelText(levelData.levelName);

        // 订阅 Player 的事件
        if (player != null)
            player.OnFoodCollected += CollectFood;
    }

    //计时逻辑
    void Update()
    {
        if (!started && Input.anyKey)
            started = true;

        if (started && !finished)
        {
            timer += Time.deltaTime;
            ui.SetTimeText(timer);
        }

        if (timer < levelData.timeLimit)
        {
            ui.SetTextColor(Color.green);
            ui.SetWinTextAndColor("EXCELLENT!",Color.green);
        }
        else if (timer < levelData.midTime)
        {
            ui.SetTextColor(new Color(1f, 0.5f, 0f));
            ui.SetWinTextAndColor("JUST MADE IT!",new Color(1f, 0.5f, 0f));
        }
        else
        {
            ui.SetTextColor(Color.red);
            ui.SetWinTextAndColor("TOO SLOW!",Color.red);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("退出游戏");
            Application.Quit();
        }
    }

    //事件回调方法
    void CollectFood()
    {
        score++;

        if (score >= totalFoods)
        {
            Win();
        }
    }

    //关卡结束后续逻辑
    void Win()
    {
        finished = true;
        started = false;

        // 禁止玩家移动，增大摩擦让球停下
        if (player != null)
        {
            player.canControl = false;
            player.rb.drag = 5.5f;
        }

        // 更新最佳时间
        levelData.bestTime = Mathf.Min(timer, levelData.bestTime);

        //更新最佳时间
        if(!PlayerPrefs.HasKey(levelData.levelIndex + "BestTime"))
        {
            PlayerPrefs.SetFloat(levelData.levelIndex + "BestTime",timer);
        }
        else if(timer < PlayerPrefs.GetFloat(levelData.levelIndex + "BestTime"))
        {
            PlayerPrefs.SetFloat(levelData.levelIndex + "BestTime",timer);
        }

        //触发事件
        if(timer <= levelData.timeLimit)
        {   
            OnWinGame?.Invoke();
        }

        //更新已解锁关卡
        if (levelData.levelIndex >= PlayerPrefs.GetInt("UnLockedLevelIndex") && timer <= levelData.timeLimit)
        {
            PlayerPrefs.SetInt("UnLockedLevelIndex", levelData.levelIndex + 1);
            Debug.Log("解锁了");
            PlayerPrefs.Save();
        }

        // 显示胜利面板
        ui.ShowOverPanel();
    }
}
