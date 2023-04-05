using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : Singleton<ActionManager>, ILogicBehaviour {

    private List<ActionBase> actionList = new List<ActionBase>();

    public void OnCreate() {
        
    }

    public void RunAction(ActionBase action) {
#if CLIENT_LOGIC
        actionList.Add(action);
#else
    OnLogicFrameUpdate();
#endif
    }

    public void OnLogicFrameUpdate() {
        for (int i = 0; i < actionList.Count; i++) {
            actionList[i].OnLogicFrameUpdate();
        }
        for (int i = actionList.Count - 1; i >= 0; i--) {
            if (actionList[i].actionComplete) {
                actionList.RemoveAt(i);
            }
        }
    }

    public void OnDestroy() {
        
    }

}
