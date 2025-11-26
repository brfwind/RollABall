using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public AudioManager music;
    public bool move = false;
    public float speed = 5f;
    public bool canControl = true;

    //事件
    public event Action OnFoodCollected;

    void Start()
    {
        music = AudioManager.instance;
    }

    //只有小球有水平速度时，才开始计时
    //move会在levelManager里被使用
    void Update()
    {
        Vector3 v = rb.velocity;

        Vector2 hv = new Vector2(v.x,v.z);

        float hSpeed = hv.magnitude;

        if(hSpeed > 0.1f)
            move = true;
        else 
            move = false; 
    }

    //实现小球移动
    void FixedUpdate()
    {
        if (!canControl)
            return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0, v) * speed;
        rb.AddForce(dir);
    }

    //碰撞食物检测
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            music.PlayFood();
            OnFoodCollected?.Invoke(); // 通知 LevelManager
        }
    }
}
