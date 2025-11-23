using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "MyGame/Level Data")]
public class LevelData : ScriptableObject
{
    public string levelName;     // 关卡名
    public float timeLimit;      // 时间限制
    public float midTime;
    public float longTime;
    public float bestTime;       // 玩家历史最佳时间
}
