using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicTimerManager : SingletonMono<LogicTimerManager>, ILogicBehaviour {

    private List<LogicTimer> logicTimers = new List<LogicTimer>();

    public void OnCreate() {

    }

    public void DelayCall(VInt delayTime, Action callback, int loop = 1) {
#if CLIENT_LOGIC
        var logicTimer = new LogicTimer(delayTime, callback, loop);
        logicTimers.Add(logicTimer);
#else
        for (int i = 0; i < loop; i++) {
            callback?.Invoke();
        }
#endif
    }

    public void OnLogicFrameUpdate() {
        for (int i = 0; i < logicTimers.Count; i++) {
            logicTimers[i].OnLogicFrameUpdate();
        }
        for (int i = logicTimers.Count - 1; i >= 0; i--) {
            if (logicTimers[i].finished) {
                logicTimers.RemoveAt(i);
            }
        }
    }

    public void OnDestroy() {
        logicTimers.Clear();
    }


}
