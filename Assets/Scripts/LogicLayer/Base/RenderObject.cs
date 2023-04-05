using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderObject : MonoBehaviour
{
    public LogicObject LogicObj { get; private set; }

    public virtual void SetLogicObject(LogicObject logicObject) {
        LogicObj = logicObject;
    }

    public virtual void Update() {
        if (LogicObj == null) {
            return;
        }
        transform.position = Vector3.Lerp(transform.position, LogicObj.LogicPosition.vec3, BattleWorld.deltaTime);
    }

    public virtual void OnRelease() {
        GameObject.Destroy(gameObject);
    }
}
