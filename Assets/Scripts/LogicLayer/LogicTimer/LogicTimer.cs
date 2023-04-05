using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicTimer : ILogicBehaviour {

    public bool finished;
    public VInt delayTime;
    public int loopCount;
    public Action OnTimerComplete;
    private VInt curTotalTime;

    public LogicTimer(VInt delayTime, Action callback, int loop = 1) {
        this.delayTime = delayTime;
        this.loopCount = loop;
        this.OnTimerComplete = callback;
    }
    public void OnCreate() {
        
    }

    public void OnDestroy() {
        
    }

    public void OnLogicFrameUpdate() {
        curTotalTime += (VInt)LogicFrameSyncConfig.LogicFrameIntvertalms;
        if (curTotalTime >= delayTime && loopCount > 0) {
            OnTimerComplete?.Invoke();
            curTotalTime = 0;
            loopCount--;
            if (loopCount == 0) {
                finished = true;
            }
        }
    }

}
