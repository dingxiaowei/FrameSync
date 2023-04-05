using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LogicObjectState {
    Survival,
    Death,
    SurvivalWaiting
}

public class LogicObject : LogicBehaviour {

    public LogicObjectState objectState = LogicObjectState.Survival;

    public void SetRenderObject(RenderObject renderObject) {
        objectState = LogicObjectState.Survival;
        RenderObj = renderObject;
        LogicPosition = new VInt3(renderObject.gameObject.transform.position);
    }

    public override void OnDestroy() {
        base.OnDestroy();
#if CLIENT_LOGIC
        RenderObj.OnRelease();
#endif
    }
}