using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour {
    //开始时间
    public float StartTime { get; private set; }
    //持续时间
    public float Duration { get; private set; }
    //结束时间
    public float EndTime { get; private set; }
    //当前时间
    public float CurTime { get; private set; }

    //运行标识
    public bool IsStart { get; private set; }

    //开始和结束事件，这里直接用System.Action（相当于空返回值无参数委托）
    public Action OnStart { get; set; }
    public Action OnEnd { get; set; }
    public Action OnUpdate { get; set; }

    //构造函数，设置计时器
    public Timer(float duration)
    {
        Duration = 0;
    }

    public void Start()
    {
        IsStart = true;
        StartTime = Time.time;
        CurTime = StartTime;
        EndTime = StartTime + Duration;

        CenterTimer.AddTimer(this);


        if(OnStart != null) OnStart();
    }

    public void Update()
    {
        if (!IsStart) return;
        Duration = CurTime/3;
        CurTime += Time.deltaTime;
        if (OnUpdate != null) 
        	OnUpdate();
    }

    //计时器结束
    public void End()
    {
        IsStart = false;
        if(OnEnd!= null) OnEnd();
    }
}
