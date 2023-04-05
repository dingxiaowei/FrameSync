using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : BulletBase
{
    public BulletLogic(LogicObject attacker, LogicObject target, VInt flyTime, Action hitComplete) : base(attacker, target, flyTime, hitComplete) {

    }

    public override void OnCreate() {
        base.OnCreate(); 
        MoveToAction action = new MoveToAction(this, attackTarget.LogicPosition, flyTime, BulletMoveComplete);
        ActionManager.Instance.RunAction(action);
    }

    public void BulletMoveComplete() {
        OnHitComplete?.Invoke();
        BulletManager.Instance.RemoveBullet(this);
    }

    public override void OnDestroy() {
        base.OnDestroy();
#if RENDER_LOGIC
        RenderObj.OnRelease();
#endif
    }
}
