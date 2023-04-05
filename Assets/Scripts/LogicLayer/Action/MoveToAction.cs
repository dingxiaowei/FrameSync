using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToAction : ActionBase {

    private LogicObject moveObject;
    private VInt3 targetPos;
    private VInt timeMs;
    public Action OnMoveComplete;

    private VInt lerpTime;
    private VInt3 originPos;

    public MoveToAction(LogicObject moveObject, VInt3 targetPos, VInt timeMs, Action moveComplete) {
        this.moveObject = moveObject;
        originPos = moveObject.LogicPosition;
        this.targetPos = targetPos;
        this.timeMs = timeMs;
        this.OnMoveComplete = moveComplete;
    }

    public override void OnLogicFrameUpdate() {
        base.OnLogicFrameUpdate();
#if CLIENT_LOGIC
        lerpTime += (VInt)LogicFrameSyncConfig.LogicFrameIntvertalms;
        VInt lerpValue = lerpTime / timeMs;
        moveObject.LogicPosition = VInt3.Lerp(originPos, targetPos, lerpValue.RawFloat);
        if (lerpValue >= VInt.one) {
            OnMoveComplete?.Invoke();
            actionComplete = true;
            return;
        }
#else
        OnMoveComplete?.Invoke();
        actionComplete = true;
#endif
    }
}
