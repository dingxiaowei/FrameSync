using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillState {
    None,
    ShakeBefore,
    ShakeAfter,
}

public class Skill {
    
    public SkillConfig SkillConfig { get; private set; }
    public int SkillId { get; private set; }
    private HeroLogic skillOwner;
    private HeroLogic skillTarget;
    private bool isNormalAtk;
    public SkillState SkillState { get; private set; }

    public Skill(int skillId, LogicObject skillOwner, bool isNormalAtk) {
        SkillId = skillId;
        this.skillOwner = (HeroLogic)skillOwner;
        this.isNormalAtk = isNormalAtk;
        SkillConfig = SkillConfigCenter.LoadSkillConfig(skillId);
    }

    public void ReleaseSkill() {
        Debugger.Log("release skillId = " + SkillId);
        SkillShakeBefore();
        PlaySkillAnim();
        if (SkillConfig.skillType == SkillType.MoveToAttack || SkillConfig.skillType == SkillType.MoveToCenter || SkillConfig.skillType == SkillType.MoveToEnemyCenter) {
            MoveToTarget(SkillTrigger);
        } else if (SkillConfig.skillType == SkillType.Chant) {
            SkillChant(SkillTrigger);
        } else if (SkillConfig.skillType == SkillType.Ballistic) {
            LogicTimerManager.Instance.DelayCall(SkillConfig.skillShakeBeforeTimeMs, CreateBullet);
        }
    }

    public void SkillShakeBefore() {
        SkillState = SkillState.ShakeBefore;
    }

    public void PlaySkillAnim() {
        skillOwner.PlayAnim(SkillConfig.skillAnim);
    }

    public void CreateBullet() {
        var skillTarget = BattleRule.GetNormalAttackTarget(WorldManager.BattleWorld.heroLogic.GetHeroListByTeam(skillOwner, (HeroTeamEnum)SkillConfig.targetType), skillOwner.HeroData.seatid);
        BulletManager.Instance.CreateBullet(SkillConfig.bullet, skillOwner, skillTarget, SkillConfig.skillAttackDurationMs, SkillTrigger);
    }

    public void SkillChant(Action chantFinish) {
        LogicTimerManager.Instance.DelayCall(SkillConfig.skillShakeBeforeTimeMs, chantFinish);
    }

    public void MoveToTarget(Action moveFinish) {
        VInt3 targetPos = VInt3.zero;
        if (SkillConfig.skillType == SkillType.MoveToAttack) {
            skillTarget = BattleRule.GetNormalAttackTarget(WorldManager.BattleWorld.heroLogic.GetHeroListByTeam(skillOwner, (HeroTeamEnum)SkillConfig.targetType), skillOwner.HeroData.seatid);
            targetPos = new VInt3(skillTarget.LogicPosition.x, skillTarget.LogicPosition.y, skillTarget.LogicPosition.z);
            VInt z = skillOwner.HeroTeam == HeroTeamEnum.Enemy ? new VInt(-3).Int : new VInt(3).Int;
            targetPos.z -= z.RawInt;
        } else if (SkillConfig.skillType == SkillType.MoveToEnemyCenter) {
            targetPos = new VInt3(skillOwner.HeroTeam == HeroTeamEnum.Enemy ? BattleWorldNodes.Instance.selfHeroCenterTrans.position : BattleWorldNodes.Instance.enemyCenterTrans.position);

        } else if (SkillConfig.skillType == SkillType.MoveToCenter) {
            targetPos = new VInt3(BattleWorldNodes.Instance.centerTrans.position);
        }
        var action = new MoveToAction(skillOwner, targetPos, SkillConfig.skillShakeBeforeTimeMs, moveFinish);
        ActionManager.Instance.RunAction(action);
    }

    public void SkillTrigger() {
        if (isNormalAtk) {
            skillOwner.UpdateAnger(skillOwner.HeroData.atkRage);
        }
        List<HeroLogic> herolist = TakeDamage();
        CreateSkillEffect(herolist);
        AddBuff(herolist);
        SkillShakeAfter();
        if (SkillConfig.skillAttackDurationMs > 0) {
            LogicTimerManager.Instance.DelayCall(SkillConfig.skillAttackDurationMs, () => { MoveToSeat(SkillEnd); });
        } else {
            MoveToSeat(SkillEnd);
        }
    }

    public void CreateSkillEffect(List<HeroLogic> herolist) {
#if RENDER_LOGIC
        if (!string.IsNullOrEmpty(SkillConfig.skillHitEffect)) {
            for (int i = 0; i < herolist.Count; i++) {
                SkillEffect skillEffect = ResourceManager.Instance.LoadObject<SkillEffect>(AssetPathConfig.SKILL_EFFECT + SkillConfig.skillHitEffect);
                skillEffect.SetEffectPos(herolist[i].LogicPosition);
            }
        }
        if (!string.IsNullOrEmpty(SkillConfig.skillEffect)) {
            SkillEffect skillEffect = ResourceManager.Instance.LoadObject<SkillEffect>(AssetPathConfig.SKILL_EFFECT + SkillConfig.skillEffect);
            if (skillOwner.HeroTeam == HeroTeamEnum.Enemy) {
                Vector3 angle = skillEffect.transform.eulerAngles;
                angle.y = 180;
                skillEffect.transform.eulerAngles = angle;
            }
            if (SkillConfig.skillAttackType == SkillAttackType.AllHero) {
                skillEffect.SetEffectPos(VInt3.zero);
            } else {
                skillEffect.SetEffectPos(skillOwner.LogicPosition);
            }
        }
#endif
    }

    public List<HeroLogic> TakeDamage() {
        List<HeroLogic> herolist = WorldManager.BattleWorld.heroLogic.GetHeroListByTeam(skillOwner, (HeroTeamEnum)SkillConfig.targetType);
        List<HeroLogic> attackHeroList = BattleRule.GetAttackListByAttackType(SkillConfig.skillAttackType, herolist, skillOwner.HeroData.seatid);
        foreach (HeroLogic hero in attackHeroList) {
            VInt damage = BattleRule.CalculateDamgae(SkillConfig, skillOwner, hero);
            hero.UpdateAnger(hero.HeroData.takeDamageRage);
            if (damage != 0) {
                if (SkillConfig.targetType == TargetType.Teammate) {
                    hero.DamageHP(-damage);
                } else {
                    hero.DamageHP(damage);
                }
                Debugger.Log("damage : " + damage.RawInt);
            }
        }
        return attackHeroList;
    }

    public void AddBuff(List<HeroLogic> targetList) {
        if (SkillConfig.addBuffs != null && SkillConfig.addBuffs.Length > 0) {
            foreach (var target in targetList) {
                for (int i = 0; i < SkillConfig.addBuffs.Length; i++) {
                    BuffManager.Instance.CreateBuff(SkillConfig.addBuffs[i], skillOwner, target);
                }
            }
        }
    }

    public void SkillShakeAfter() {
        SkillState = SkillState.ShakeAfter;
    }

    public void MoveToSeat(Action moveFinish) {
        if (SkillConfig.skillType == SkillType.Chant || SkillConfig.skillType == SkillType.Ballistic) {
            LogicTimerManager.Instance.DelayCall(SkillConfig.skillShakeAfterTimeMs, moveFinish);
        } else {
            VInt3 seatPos = VInt3.zero;
#if CLIENT_LOGIC
            Transform[] seatTransArr = skillOwner.HeroTeam == HeroTeamEnum.Enemy ? BattleWorldNodes.Instance.enemyTransArr : BattleWorldNodes.Instance.heroTransArr;
            seatPos = new VInt3(seatTransArr[skillOwner.HeroData.seatid].position);
#endif
            var action = new MoveToAction(skillOwner, seatPos, SkillConfig.skillShakeAfterTimeMs, moveFinish);
            ActionManager.Instance.RunAction(action);
        }

    }

    public void SkillEnd() {
        Debugger.Log("Skillend " + SkillId);
        skillOwner.ActionEnd();
    }
}
