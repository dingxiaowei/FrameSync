using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : SingletonMono<BuffManager>, ILogicBehaviour {

    private List<BuffLogic> buffList = new List<BuffLogic>();

    public void OnCreate() {
        
    }

    public BuffLogic CreateBuff(int buffId, LogicObject owner, LogicObject target) {
        BuffLogic buff = new BuffLogic(buffId, owner, target);
        buff.OnCreate();
        buffList.Add(buff);
        return buff;
    }

    public void OnDestroy() {
        for (int i = 0; i < buffList.Count; i++) {
            buffList[i].OnDestroy();
        }
        buffList.Clear();
    }

    public void OnLogicFrameUpdate() {
        for (int i = 0; i < buffList.Count; i++) {
            buffList[i].OnLogicFrameUpdate();
        }
        for (int i = buffList.Count - 1; i >= 0; i--) {
            BuffLogic buff = buffList[i];
            if (buff.objectState == LogicObjectState.Death) {
                buff.OnDestroy();
                buffList.RemoveAt(i);
            }
        }
    }

    public void RemoveBuff(BuffLogic buff) {
        if (buffList.Contains(buff)) {
            buffList.Remove(buff);
        }
    }

    public void DestroyBuff(BuffLogic buff) {
        buff.targetHero.RemoveBuff(buff);
    }
}
