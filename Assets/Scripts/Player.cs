using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    public Rigidbody rb;
    public TextMeshProUGUI textComponent;
    public GameObject winTextGo;
    public float speed;
    public float Score = 0;

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h,0,v) * speed;

        rb.AddForce(dir);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("退出游戏"); // 在编辑器里测试用
            Application.Quit();     // 打包后才有效
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Food")
        {
            Score++;
            textComponent.text = "Score:" + Score.ToString();
            Destroy(collision.gameObject);

            if(Score == 15)
            {
                winTextGo.SetActive(true);
            }
        }
    }
}
