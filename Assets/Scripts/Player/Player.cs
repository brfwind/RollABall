using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 5f;
    public bool canControl = true;

    //事件
    public event Action OnFoodCollected;

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
            OnFoodCollected?.Invoke(); // 通知 LevelManager
        }
    }
}
