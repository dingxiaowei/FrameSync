using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicBehaviour {
    public RenderObject RenderObj { get; protected set; }

    public VInt3 LogicPosition { get; set; }
    public Action OnActionEnd { get; set; }

    public virtual void OnCreate() { }

    public virtual void OnLogicFrameUpdate() { }

    public virtual void OnDestroy() { }

    public virtual void RoundStartEvent(int round) { }

    public virtual void RoundEndEvent() { }

    public virtual void ActionStart() { }

    public virtual void ActionEnd() { }
}
