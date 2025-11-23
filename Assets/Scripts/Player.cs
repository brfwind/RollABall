using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public Rigidbody rb;
    private int totalFoods;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI levelText;
    public GameObject winPanel;
    /// <summary>
    /// 是否开始计时
    /// </summary>
    private bool started = false;
    /// <summary>
    /// 是否终止计时
    /// </summary>
    private bool finished = false;
    public float speed;
    private float timer = 0f;
    public float Score = 0;
    private bool canControl = true;

    void Start()
    {
        //自动获取场景中的食物总数
        totalFoods = GameObject.FindGameObjectsWithTag("Food").Length;
        levelText.text = SceneManager.GetActiveScene().name;
    }
    void FixedUpdate()
    {
        if(!canControl)
            return;

        //读取键盘输入
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h,0,v) * speed;

        //给小球施加力
        rb.AddForce(dir);
    }
    void Update()
    {
        if(!started && Input.anyKey)
            started = true;

        if(started && !finished)
        {
            timer += Time.deltaTime;
            timeText.text = "Time:" + timer.ToString("F2");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("退出游戏"); // 在编辑器里测试用
            Application.Quit();     // 打包后才有效
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        //检测到碰到food：
        //加得分
        //销毁food
        //符合条件时，停止计时，放结束面板
        if(collision.gameObject.tag == "Food")
        {
            Score++;
            scoreText.text = "Score:" + Score.ToString();
            Destroy(collision.gameObject);

            //结束面板
            //调整计时、禁用输入、增大摩擦使之停下
            if(Score == totalFoods)
            {
                winPanel.SetActive(true);
                finished = true;
                started = false;
                canControl = false;
                rb.drag = 5.5f;
            }
        }
    }
}
