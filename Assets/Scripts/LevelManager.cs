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

        if(timer < levelData.timeLimit)
        {
            ui.SetTextColor(Color.green);
        }
        else if(timer < levelData.midTime)
        {
            ui.SetTextColor(new Color(1f, 0.5f, 0f));
        }
        else
        {
            ui.SetTextColor(Color.red);
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

        // 显示胜利面板
        ui.ShowOverPanel();
    }
}
