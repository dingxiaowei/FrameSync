using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffLogic : LogicObject
{
    public BuffConfig BuffConfig { get; private set; }

    public int BuffId { get; private set; }
    protected LogicObject owner;
    public HeroLogic ownerHero;
    public HeroLogic targetHero;
    protected LogicObject target;

    public int curAccTime;
    private int curRealTime;
    private int curSurivalRoundCount;

    public BuffLogic(int buffId, LogicObject owner, LogicObject target) {
        BuffId = buffId;
        this.owner = owner;
        this.ownerHero = owner as HeroLogic;
        this.target = target;
        this.targetHero = target as HeroLogic;
    }

    public override void OnCreate() {
        base.OnCreate();
        objectState = LogicObjectState.Survival;
        BuffConfig = SkillConfigCenter.LoadBuffConfig(BuffId);
        if (BuffConfig.triggerType == BuffTriggerType.DamgeRoundStart || BuffConfig.triggerType == BuffTriggerType.DamgeRoundEnd) {
            AddBuffEffect();
        }
    }

    public override void OnLogicFrameUpdate() {
        base.OnLogicFrameUpdate();
        if (objectState == LogicObjectState.Survival) {
            switch (BuffConfig.triggerType) {
                case BuffTriggerType.OneDamageRealTime:
                    if (BuffConfig.buffDurationTimeMs == 0 && BuffConfig.buffTriggerIntervalMs == 0) {
                        TriggerBuff();
                        AddBuffEffect();
                        if (BuffConfig.buffDurationRound == 0) {
                            objectState = LogicObjectState.Death;
                        } else {
                            objectState = LogicObjectState.SurvivalWaiting;
                        }
                    } else {
                        curRealTime += LogicFrameSyncConfig.LogicFrameIntvertalms;
                        if (curRealTime >= BuffConfig.buffTriggerIntervalMs) {
                            TriggerBuff();
                            AddBuffEffect();
                            curRealTime -= BuffConfig.buffTriggerIntervalMs;
                        }
                        curAccTime += LogicFrameSyncConfig.LogicFrameIntvertalms;
                        if (curAccTime >= BuffConfig.buffDurationTimeMs) {
                            objectState = LogicObjectState.Death;
                            break;
                        }
                    }
                    break;
                case BuffTriggerType.MultiDamageRealTime:
                    if (BuffConfig.buffDurationTimeMs > 0 && BuffConfig.buffTriggerIntervalMs > 0) {
                        curRealTime += LogicFrameSyncConfig.LogicFrameIntvertalms;
                        if (curRealTime >= BuffConfig.buffTriggerIntervalMs) {
                            TriggerBuff();
                            AddBuffEffect();
                            curRealTime -= BuffConfig.buffTriggerIntervalMs;
                        }
                        curAccTime += LogicFrameSyncConfig.LogicFrameIntvertalms;
                        if (curAccTime >= BuffConfig.buffDurationTimeMs) {
                            objectState = LogicObjectState.Death;
                            break;
                        }
                    }
                    break;
                case BuffTriggerType.DamgeRoundStart:
                    break;
                case BuffTriggerType.DamgeRoundEnd:
                    break;
            }
        }
    }

    public override void RoundStartEvent(int round) {
        base.RoundStartEvent(round);
    }

    public override void RoundEndEvent() {
        base.RoundEndEvent();
        if (objectState == LogicObjectState.Survival) {
            if (BuffConfig.triggerType == BuffTriggerType.DamgeRoundEnd) {
                TriggerBuff();
            }
        }
        curSurivalRoundCount++;
        if (objectState == LogicObjectState.Survival || objectState == LogicObjectState.SurvivalWaiting) {
            if (BuffConfig.buffDurationRound >= 0 && curSurivalRoundCount >= BuffConfig.buffDurationRound) {
                targetHero.SetAnimState(AnimState.RePlayAnim);
                objectState = LogicObjectState.Death;
                OnDestroy();
            }
        }
    }

    public void TriggerBuff() {
        if (BuffConfig.damageType != BuffDamageType.None) {
            VInt damage = BattleRule.CalculateDamgae(BuffConfig, (HeroLogic)owner, (HeroLogic)target);
            HeroLogic targetHero = (HeroLogic)target;
            targetHero.BuffDamage(damage, BuffConfig);
        }
    }

    public void AddBuffEffect() {
        bool isTrigger = BuffConfig.buffTriggerProbability >= 100;
        if (BuffConfig.buffTriggerProbability > 0 && BuffConfig.buffTriggerProbability < 100) {
            int result = LogicRandom.Instance.Range(0, 100);
            if (result < BuffConfig.buffTriggerProbability) {
                isTrigger = true;
            }
        }
        if (isTrigger) {
            int alreadyBuffCount = targetHero.GetBuffCount(BuffId);
            if (!string.IsNullOrEmpty(BuffConfig.buffEffect) && alreadyBuffCount == 0) {
                RenderObj = ResourceManager.Instance.LoadObject<RenderObject>(AssetPathConfig.BUFF_EFFECT + BuffConfig.buffEffect);
                SetRenderObject(RenderObj);
                RenderObj.SetLogicObject(target);
            }
            if (alreadyBuffCount != 0) {
                targetHero.RefreshBuffDurationRound(BuffId);
            }
            if (BuffConfig.buffType == BuffType.Control) {
                targetHero.SetAnimState(AnimState.StopAnim);
            }
            targetHero.AddBuff(this);
        }
        
    }

    public void RefreshBuffDurationRound() {
        curSurivalRoundCount = 0;
    }

    public override void OnDestroy() {
        objectState = LogicObjectState.Death;
        RenderObj?.OnRelease();
        BuffManager.Instance.DestroyBuff(this);
    }
}
