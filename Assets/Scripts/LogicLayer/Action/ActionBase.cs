using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBase : ILogicBehaviour
{
    public bool actionComplete = false;

    public void OnCreate() {
        
    }

    public void OnDestroy() {
    
    }

    public virtual void OnLogicFrameUpdate() {

    }

    
}
