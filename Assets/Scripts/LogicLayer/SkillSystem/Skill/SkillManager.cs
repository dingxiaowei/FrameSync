using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>, ILogicBehaviour {
    public void OnCreate() {
        
    }

    public Skill ReleaseSkill(int skillId, LogicObject skillOwner, bool isNormalAtk) {
        var skill = new Skill(skillId, skillOwner, isNormalAtk);
        skill.ReleaseSkill();
        return skill;
    }

    public void OnDestroy() {
        
    }

    public void OnLogicFrameUpdate() {
        
    }

}
