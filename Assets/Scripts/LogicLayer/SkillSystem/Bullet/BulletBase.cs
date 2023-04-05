using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : LogicObject {

    protected LogicObject attacker;
    protected LogicObject attackTarget;
    protected VInt flyTime;
    protected Action OnHitComplete;

    public BulletBase(LogicObject attacker, LogicObject target, VInt flyTime, Action hitComplete) {
        this.attacker = attacker;
        this.attackTarget = target;
        this.flyTime = flyTime;
        this.OnHitComplete = hitComplete;
    }

}
