using System.Collections.Generic;
using System.Linq;

public enum AnimState {
    StopAnim,
    RePlayAnim
}
public class HeroLogic : LogicObject {

    protected VInt hp;
    public VInt atk;
    public VInt def;
    protected VInt agl;
    protected VInt rage;

    public VInt addDef;
    public VInt HP { get { return hp; } }
    public VInt MaxHP { get; protected set; }
    public VInt Atk { get { return atk; } }
    public VInt Def { get { return def + addDef; } }
    public VInt Agl { get { return agl; } }
    public VInt Rage { get { return rage; } }
    public VInt MaxRage { get { return 100; } }

    public int ID => HeroData.id;
    public HeroData HeroData { get; private set; }
#if RENDER_LOGIC
    public HeroRender HeroRender { get; private set; }
#endif
    public HeroTeamEnum HeroTeam { get; private set; }

    public List<BuffLogic> haveBuffList = new List<BuffLogic>();

    public HeroLogic(HeroData heroData, HeroTeamEnum heroTeam) {
        HeroData = heroData;
        HeroTeam = heroTeam;
        hp = heroData.hp;
        atk = heroData.atk;
        def = heroData.def;
        agl = heroData.agl;
        MaxHP = hp;
        rage = 0;
    }
    public override void OnCreate() {
        base.OnCreate();
#if RENDER_LOGIC
        HeroRender = (HeroRender)RenderObj;
#endif
        UpdateAnger(rage);
        //Debugger.Log("heroname = " + RenderObj.gameObject.name);
    }

    public override void OnLogicFrameUpdate() {
        base.OnLogicFrameUpdate();
    }

    public override void ActionStart() {
        base.ActionStart();
        if (objectState == LogicObjectState.Death || IsBeControl()) {
            ActionEnd();
            return;
        }
        bool isNormal = rage < MaxRage;
        if (rage >= MaxRage) {
            rage = 0;
        }
        int skillId = isNormal ? HeroData.skillidArr[0] : HeroData.skillidArr[1];
        SkillManager.Instance.ReleaseSkill(skillId, this, isNormal);
        UpdateAnger(0);
    }

    public override void ActionEnd() {
        base.ActionEnd();
        OnActionEnd?.Invoke();
    }

    public void DamageHP(VInt damageHp, BuffConfig buffConfig = null) {
        if (damageHp == 0) {
            return;
        }
        hp -= damageHp;
        if (hp <= 0) {
            hp = 0;
            HeroDeath();
            return;
        } else {
            if (damageHp > 0) {
                PlayAnim("OnHit");
            }
        }
        Debugger.Log("id = " + ID + ", 损失血量 = " + damageHp + ", 剩余血量 = " + hp);
#if RENDER_LOGIC
        float hpRate = hp.RawFloat / MaxHP.RawFloat;
        HeroRender.UpdateHPHud(damageHp.RawInt, hpRate);
#endif
    }

    public override void RoundStartEvent(int round) {
        base.RoundStartEvent(round);
        for (int i = haveBuffList.Count - 1; i >= 0; i--) {
            haveBuffList[i].RoundStartEvent(round);
        }
    }

    public override void RoundEndEvent() {
        base.RoundEndEvent();
        for (int i = haveBuffList.Count - 1; i >= 0; i--) {
            haveBuffList[i].RoundEndEvent();
        }
    }

    public void UpdateAnger(VInt anger) {
        rage += anger;
        if (rage > MaxRage) {
            rage = MaxRage;
        }
#if RENDER_LOGIC
        float rate = (rage / MaxRage).RawFloat;
        HeroRender.UpdateAngerHud(rate);
#endif
    }

    public void TryClearRage() {
        if (rage >= MaxRage) {
            rage = 0;
        }
    }

    private void HeroDeath() {
        objectState = LogicObjectState.Death;
#if RENDER_LOGIC
        HeroRender.Death();
        SetAnimState(AnimState.RePlayAnim);
#endif
        ClearBuff();
    }

    public void PlayAnim(string animName) {
#if RENDER_LOGIC
        HeroRender.PlayAnim(animName);
#endif
    }

    public void SetAnimState(AnimState state) {
#if RENDER_LOGIC
        HeroRender.SetAnimState(state);
#endif
    }

    public int GetBuffCount(int buffId) {
        int result = 0;
        for (int i = 0; i < haveBuffList.Count; i++) {
            if (buffId == haveBuffList[i].BuffId) {
                result++;
            }
        }
        return result;
    }

    public void RefreshBuffDurationRound(int buffId) {
        for (int i = 0; i < haveBuffList.Count; i++) {
            if (haveBuffList[i].BuffId == buffId) {
                haveBuffList[i].RefreshBuffDurationRound();
            }
        }
    }

    public void BuffDamage(VInt hp, BuffConfig buffConfig) {
        Debugger.Log("buffdamage = " + hp);
        DamageHP(hp, buffConfig);
    }

    public bool IsBeControl() {
        bool result = haveBuffList.Any(t => t.BuffConfig.buffType == BuffType.Control);
        if (result) {
            Debugger.Log(HeroData.name + " 被冰冻");
        }
        return result;
    }

    public void AddBuff(BuffLogic buff) {
        int buffId = buff.BuffId;
        if (buff.BuffConfig.maxStack >= 1) {
           int count = GetBuffCount(buffId);
            if (count >= buff.BuffConfig.maxStack) {
                return;
            }
            haveBuffList.Add(buff);
            if (buff.BuffConfig.buffState == BuffState.DefAdd) {
                 addDef += BattleRule.CalculateAddDef(buff.BuffConfig.damagePercentage, this).RawInt;
            }
        } else {
            haveBuffList.Add(buff);
        }
    }

    public void RemoveBuff(BuffLogic buff) {
        if (haveBuffList.Contains(buff)) {
            if (buff.BuffConfig.buffState == BuffState.DefAdd) {
                addDef -= BattleRule.CalculateAddDef(buff.BuffConfig.damagePercentage, this).RawInt;
            }
            haveBuffList.Remove(buff);
        }
    }

    public void ClearBuff() {
        for (int i = haveBuffList.Count - 1; i >= 0; i--) {
            haveBuffList[i].OnDestroy();
        }
    }

    public override void OnDestroy() {
        base.OnDestroy();
        OnActionEnd = null;
        ClearBuff();
    }
}
