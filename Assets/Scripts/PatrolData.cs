using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolData : MonoBehaviour
{
    public int PatSec;                      //标志巡逻兵在哪一块区域
    public bool followPlayer = false;    //是否跟随玩家
    public int PlySec = -1;            //当前玩家所在区域标志
    public GameObject player;             //玩家游戏对象
    public Vector3 startPosition;        //当前巡逻兵初始位置
    public int rangeX;
    public int rangeZ;
}
